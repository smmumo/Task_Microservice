using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Product.Application.Application.Data;
using Product.Domain.Entity;
using Product.Domain.Repository;


namespace Product.Infrastructure.Repository
{
    public sealed class ProductRepository : IProductRepository
    {
        private readonly IDbContext _dbContext;
        public ProductRepository(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public void Add(Products product)
        {
            _dbContext.Set<Products>().Add(product);
        }

        public void Delete(Products product)
        {
            _dbContext.Set<Products>().Remove(product);
        }
    }
}