using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Order.Application.Data;
using Order.Domain.Entity;
using Order.Domain.Repository;

namespace Order.Infrastructure.Repository
{
    public sealed class OrderRepository : IOrderRepository
    {
        private readonly IDbContext _dbContext;
        public OrderRepository(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public void Add(Orders order)
        {
            _dbContext.Set<Orders>().Add(order);
        }

        public void Delete(Orders order)
        {
            _dbContext.Set<Orders>().Remove(order);
        }
    }
}