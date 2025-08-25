using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Product.Domain.Entity;


namespace Product.Domain.Repository
{
    public interface IProductRepository
    {
        void Add(Products product);
        void Delete(Products product);
        Task<Products?> GetByIdAsync(Guid id);
        Task UpdateAsync(Products product);
    }
}