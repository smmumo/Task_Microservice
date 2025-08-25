using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Abstractions.Messaging;
using Order.Application.Data;
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

        public CreateOrderCommandHandler(IOrderRepository orderRepository, IUnitOfWork unitOfWork, IProductService productService)
        {
            _orderRepository = orderRepository;
            _unitOfWork = unitOfWork;
            _productService = productService;
        }

        public async Task<Result> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            //get product from Product.Api
            var product = await _productService.GetProduct(request.ProductId);

            if (product is null)
            {
                return Result.Failure(new Error("Product.NotFound", "Product not found"));
            }

            //validate qty
            if(request.Qty > product.Quantity)
            {
                return Result.Failure(new Error("Product.InvalidStockQuantity", "Insufficient stock for the requested quantity"));
            }

            var order = Orders.Create(
                request.ProductId,
                product.ProductName,
                request.TotalAmount,
                request.Qty
            );

            if(order.IsFailure)
            {
                return Result.Failure(order.Error);
            }

            _orderRepository.Add(order.Value);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}