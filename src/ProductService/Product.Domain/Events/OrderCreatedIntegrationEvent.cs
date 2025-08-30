using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Product.Domain.Events
{
    public class OrderCreatedIntegrationEvent
    {
        public Guid OrderId { get; set; }
        public string ProductName { get; set; }
        public decimal Quantity { get; set; }
    }
}