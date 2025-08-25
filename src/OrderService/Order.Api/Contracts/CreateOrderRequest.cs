using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Order.Api.Contracts
{
    public record CreateOrderRequest(   Guid ProductId,
        //string ProductName,
        decimal TotalAmount,
        decimal Qty);
   
}