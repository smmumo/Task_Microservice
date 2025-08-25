using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Product.Api.Contracts
{
    public record CreateProductRequest(       
        string ProductName,       
        decimal Qty);
   
}