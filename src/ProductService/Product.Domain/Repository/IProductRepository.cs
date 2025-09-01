using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Product.Domain.Entity;


namespace Product.Domain.Repository
{
    public interface IProductRepository
    {
        void Add(ProductEntity product);
        void Delete(ProductEntity product);
        Task<ProductEntity?> GetByIdAsync(Guid id);
        void Update(ProductEntity product);
    }
}