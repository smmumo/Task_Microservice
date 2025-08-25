using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Product.Application.Abstractions.Messaging;
using Product.Application.Application.Data;
using Product.Domain.Core;
using Product.Domain.Repository;

namespace Product.Application.CommandHandler.UpdateProduct
{
    public class UpdateStockQuantityCommandHandler : ICommandHandler<UpdateStockQuantityCommand, Result>
    {
        private readonly IProductRepository _productRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateStockQuantityCommandHandler(IProductRepository productRepository, IUnitOfWork unitOfWork)
        {
            _productRepository = productRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(UpdateStockQuantityCommand command, CancellationToken cancellationToken)
        {
            var product = await _productRepository.GetByIdAsync(command.ProductId);

            if (product is null)
            {
                return Result.Failure(new Error("Product.NotFound", "Product not found."));
            }            

            var result = product.UpdateStockQuantity(command.Quantity);

            if (result.IsFailure)
            {
                return result;
            }

             _productRepository.Update(product);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}