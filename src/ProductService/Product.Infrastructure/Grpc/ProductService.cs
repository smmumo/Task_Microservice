using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Grpc.Core;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Product.Domain.Entity;
using Product.Domain.Repository;
using ProductApiServer;


namespace Product.Infrastructure.Grpc
{

    public class ProductService : ProductGrpc.ProductGrpcBase
    {
        private readonly ProductDbContext _dbContext;
        private readonly IProductRepository _productRepository;

        public ProductService(ProductDbContext dbContext, IProductRepository productRepository)
        {
            _dbContext = dbContext;
            _productRepository = productRepository;
        }

        public override async Task<GetProductResponse> GetProduct(GetProductRequest request,
                        ServerCallContext context)
        {
            var product = await _dbContext.Set<ProductEntity>().FirstOrDefaultAsync(p => p.Id.ToString() == request.Id);
            if (product is null)
            {
                throw new RpcException(new Status(StatusCode.NotFound, $"Product with ID {request.Id} not found."));
            }

            return new GetProductResponse
            {
                Id = product.Id.ToString(),
                Name = product.ProductName,
                Quantity = (double)product.Quantity
            };
        }
        
        public override async Task<Google.Protobuf.WellKnownTypes.Empty> ReserveProduct(ReserveProductRequest request, ServerCallContext context)
        {
            var product = await _productRepository.GetByIdAsync(Guid.Parse(request.Id));

            if (product is null)
            {
                throw new RpcException(new Status(StatusCode.NotFound, $"Product with ID {request.Id} not found."));
            }

            if(product.Quantity < (decimal)request.Quantity)
            {
                throw new RpcException(new Status(StatusCode.FailedPrecondition, $"Insufficient quantity for product ID {request.Id}."));
            }

            product.Quantity -= (decimal)request.Quantity;

           _productRepository.Update(product);

            await _dbContext.SaveChangesAsync();

            return new Google.Protobuf.WellKnownTypes.Empty();
        }


        // public override async Task<CreateProductResponse> CreateProduct(CreateProductRequest request, ServerCallContext context)
        // {
        //     var result = ProductEntity.Create(request.Name, (decimal)request.Quantity);
        //     if (result.IsFailure)
        //     {
        //         throw new RpcException(new Status(StatusCode.InvalidArgument, string.Join(", ", result.Error.Message)));
        //     }

        //     var product = result.Value;

        //     _dbContext.Set<ProductEntity>().Add(product);

        //     await _dbContext.SaveChangesAsync();

        //     return new CreateProductResponse
        //     {
        //         Id = product.Id.ToString(),
        //         Name = product.ProductName,
        //         Quantity = product.Quantity
        //     };
        // }

        // public override async Task<ProductList> ListProducts(Google.Protobuf.WellKnownTypes.Empty request, ServerCallContext context)
        // {
        //     var products = await _dbContext.Set<ProductEntity>().AsNoTracking().ToListAsync();

        //     var productList = new ProductList();
        //     productList.Products.AddRange(products.Select(p => new ProductListResponse
        //     {
        //         Id = p.Id.ToString(),
        //         Name = p.ProductName,
        //         Quantity = (double)p.Quantity
        //     }));

        //     return productList;
        // }

    }
}