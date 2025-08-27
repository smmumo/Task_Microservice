using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Order.Application.DTO
{
   public record UpdateProductQuantityRequest(decimal Quantity, Guid ProductId);
}