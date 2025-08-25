using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Order.Domain.Entity;

namespace Infrastructure.Persistence.Configuration
{
    public class Order : IEntityTypeConfiguration<Orders>
    {
        public void Configure(EntityTypeBuilder<Orders> builder)
        {
            builder.Property(e => e.Id)
                .HasColumnName("Id");
            builder.HasKey(order => order.Id);
            builder.Property(order => order.ProductName).IsRequired().HasMaxLength(100);
            builder.Property(order => order.Quantity).IsRequired();
            builder.Property(order => order.Price).IsRequired().HasColumnType("decimal(65,2)");
        }
    }
}