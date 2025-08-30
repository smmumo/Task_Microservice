using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Order.Domain.Core;
using Order.Domain.Core.Results;

namespace Order.Domain.Entity
{
    public class OrderEntity : BaseEntity
    {
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        public string ProductName { get; set; } = null!;
        public decimal Price { get; set; }
        public decimal Quantity { get; set; }

        public static Result<OrderEntity> Create(Guid productId, string productName, decimal price, decimal quantity)
        {
            if (quantity <= 0)
            {
                return Result.Failure<OrderEntity>(new Error("Product.InvalidStockQuantity", "Quantity must be greater than zero."));
            }
            if (price <= 0)
            {
                return Result.Failure<OrderEntity>(new Error("Product.InvalidPrice", "Price must be greater than zero."));
            }
            if (string.IsNullOrWhiteSpace(productName))
            {
                return Result.Failure<OrderEntity>(new Error("Product.InvalidName", "Product name cannot be null or empty."));
            }

            var order = new OrderEntity
            {
                Id = Guid.NewGuid(),
                ProductId = productId,
                ProductName = productName,
                Price = price,
                Quantity = quantity
            };

            //add domain event , orderCreated
            return Result.Success(order);
        }
    }
}