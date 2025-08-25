using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;
using Product.Application.Abstractions.Messaging;
using Product.Application.Application.Data;
using Product.Application.Application.DTO;

using Product.Domain.Entity;
using Product.Domain.Repository;


namespace Product.Application.QueryHandler
{
    public class GetProductByIdQueryHandler : IQueryHandler<GetProductByIdQuery, ProductDto?>
    {
        //private readonly IProductRepository _productRepository;
        private readonly IDbContext _dbContext;

        public GetProductByIdQueryHandler(IDbContext dbContext)
        {
           // _productRepository = productRepository;
            _dbContext = dbContext;
        }

        public async Task<ProductDto?> Handle(GetProductByIdQuery query, CancellationToken cancellationToken)
        {
            var product = await _dbContext.Set<Products>()
                 .Where(p => p.Id == query.ProductId)
                .FirstOrDefaultAsync(cancellationToken);

            return product is not null ? new ProductDto(product.Id, product.ProductName, product.Quantity) : null;
        }
    }
}