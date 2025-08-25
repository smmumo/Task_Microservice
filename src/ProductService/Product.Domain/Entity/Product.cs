using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Product.Domain.Core;

namespace Product.Domain.Entity
{
    public class Products : BaseEntity
    {
        public Guid Id { get; set; }
        public string ProductName { get; set; } = null!;
        public decimal Quantity { get; set; }

        public static Result<Products> Create(string productName, decimal quantity)
        {
            if (quantity <= 0)
            {
                return Result.Failure<Products>(new Error("Product.InvalidStockQuantity", "Quantity must be greater than zero."));
            }

            if (string.IsNullOrWhiteSpace(productName))
            {
                return Result.Failure<Products>(new Error("Product.InvalidName", "Product name cannot be null or empty."));
            }

            var product = new Products
            {
                Id = Guid.NewGuid(),
                ProductName = productName,
                Quantity = quantity
            };

            return Result.Success(product);       

           
        }

        public Result UpdateStockQuantity(decimal newQuantity)
        {
            if (newQuantity <= 0)
            {
                return Result.Failure(new Error("Product.InvalidStockQuantity", "Quantity must be greater than zero."));
            }

            Quantity = newQuantity;
            return Result.Success();
        }
           
        
    }
}