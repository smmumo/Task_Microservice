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
        public void Add(ProductEntity product)
        {
            _dbContext.Set<ProductEntity>().Add(product);
        }

        public void Delete(ProductEntity product)
        {
            _dbContext.Set<ProductEntity>().Remove(product);
        }

        public async Task<ProductEntity?> GetByIdAsync(Guid id)
        {
            return await _dbContext.Set<ProductEntity>().FindAsync(id);
        }

        public void Update(ProductEntity product)
        {
            _dbContext.Set<ProductEntity>().Update(product);
        }
    }
}