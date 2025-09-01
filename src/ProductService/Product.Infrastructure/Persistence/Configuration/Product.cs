using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Product.Domain.Entity;


namespace Infrastructure.Persistence.Configuration
{
    public class Product : IEntityTypeConfiguration<ProductEntity>
    {
        public void Configure(EntityTypeBuilder<ProductEntity> builder)
        {
            builder.Property(e => e.Id)
                .HasColumnName("Id");
            builder.HasKey(product => product.Id);
            builder.Property(product => product.ProductName).IsRequired().HasMaxLength(100);
            builder.Property(product => product.Quantity).IsRequired();

        }
    }
}