using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Product.Application.Abstractions.Messaging;
using Product.Application.Application.Data;
using Product.Application.CommandHandler.CreateProduct;
using Product.Domain.Core;
using Product.Domain.Entity;
using Product.Domain.Repository;

namespace Product.Application.CommandHandler.CreateProduct
{
    public sealed class CreateProductCommandHandler : ICommandHandler<CreateProductCommand, Result>
    {
        private readonly IProductRepository _productRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateProductCommandHandler(IProductRepository productRepository, IUnitOfWork unitOfWork)
        {
            _productRepository = productRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var product = Products.Create(
                request.ProductName,
                request.Quantity
            );

            if (product.IsFailure)
            {
                return Result.Failure(product.Error);
            }

            _productRepository.Add(product.Value);

            await _unitOfWork.SaveChangesAsync(cancellationToken);           

            return Result.Success();
        }
    }
}