using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Product.Application.Application.DTO
{
    public record ProductDto(
        Guid Id,
        string ProductName,
        decimal Quantity       
    );
}