using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;
using Product.Application.Abstractions.Messaging;
using Product.Application.Application.Data;
using Product.Application.Application.DTO;
using Product.Domain.Entity;


namespace Product.Application.QueryHandler
{
    public class GetProductQueryHandler : IQueryHandler<GetProductsQuery, List<ProductDto>>
    {
        private readonly IDbContext _dbContext;

        public GetProductQueryHandler(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<ProductDto>> Handle(GetProductsQuery query, CancellationToken cancellationToken)
        {
            var products = await _dbContext.Set<Products>().ToListAsync(cancellationToken);
            return products.Select(product => new ProductDto(product.Id, product.ProductName, product.Quantity)).ToList();
        }
    }
}