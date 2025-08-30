using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Abstractions.Messaging;
using Microsoft.Extensions.Logging;
using Order.Application.Data;
using Order.Application.DTO;
using Order.Application.Services;
using Order.Domain.Core;
using Order.Domain.Core.Results;
using Order.Domain.Entity;
using Order.Domain.Repository;

namespace Order.Application.CommandHandler.CreateOrder
{
    public sealed class CreateOrderCommandHandler : ICommandHandler<CreateOrderCommand, Result>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IProductService _productService;
        private readonly ILogger<CreateOrderCommandHandler> _logger;

        public CreateOrderCommandHandler(IOrderRepository orderRepository,
        IUnitOfWork unitOfWork,
        IProductService productService,
        ILogger<CreateOrderCommandHandler> logger)
        {
            _orderRepository = orderRepository;
            _unitOfWork = unitOfWork;
            _productService = productService;
            _logger = logger;
        }

        public async Task<Result> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            //get product from Product.Api
            var product = await _productService.GetProduct(request.ProductId);

            if (product is null)
            {
                _logger.LogWarning("Product not found: {ProductId}", request.ProductId);
                return Result.Failure(new Error("Product.NotFound", "Product not found"));
            }
          
            if(request.Qty > product.Quantity)
            {
                _logger.LogWarning("Insufficient stock for product: {ProductId}. Requested: {Requested}, Available: {Available}", request.ProductId, request.Qty, product.Quantity);
                return Result.Failure(new Error("Product.InvalidStockQuantity", "Insufficient stock for the requested quantity"));
            }

            var order = OrderEntity.Create(
                request.ProductId,
                product.ProductName,
                request.TotalAmount,
                request.Qty
            );

            if(order.IsFailure)
            {
                _logger.LogError("Order creation failed for product: {ProductId}. Error: {Error}", request.ProductId, order.Error);
                return Result.Failure(order.Error);
            }

            _orderRepository.Add(order.Value);

             await _unitOfWork.SaveChangesAsync(cancellationToken);  
             
            //Todo , introduce distributed transaction eg saga            
            bool IsReservationSuccess = await _productService.ReserveProduct(new UpdateProductQuantityRequest(request.Qty, request.ProductId));

            //introduce retries , before rollback order creation
            if (!IsReservationSuccess)
            {
                // If reservation fails, we need to rollback the order creation               
                _orderRepository.Delete(order.Value);
                await _unitOfWork.SaveChangesAsync(cancellationToken);
                _logger.LogError("Product reservation failed for product: {ProductId}. Rolling back order creation.", request.ProductId);
                return Result.Failure(new Error("Product.ReservationFailed", "Failed to reserve product"));
            }

            _logger.LogInformation("Order created successfully for product: {ProductId}, OrderId: {OrderId}", request.ProductId, order.Value.Id);

            return Result.Success();            
            
        }
    }
}