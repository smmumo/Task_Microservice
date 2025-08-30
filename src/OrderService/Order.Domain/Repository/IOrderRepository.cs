using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Order.Domain.Entity;

namespace Order.Domain.Repository
{
    public interface IOrderRepository
    {
        void Add(OrderEntity order);
        void Delete(OrderEntity order);
    }
}