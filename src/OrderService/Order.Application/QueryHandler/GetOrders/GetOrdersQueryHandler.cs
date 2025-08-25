using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Abstractions.Messaging;
using Microsoft.EntityFrameworkCore;
using Order.Application.Data;
using Order.Application.DTO;
using Order.Domain.Entity;

namespace Order.Application.QueryHandler
{
    public class GetOrdersQueryHandler : IQueryHandler<GetOrdersQuery, List<OrderDto>>
    {
        private readonly IDbContext _dbContext;

        public GetOrdersQueryHandler(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<OrderDto>> Handle(GetOrdersQuery query, CancellationToken cancellationToken)
        {
            var orders = await _dbContext.Set<Orders>().ToListAsync(cancellationToken);
            return orders.Select(order => new OrderDto(order.Id, order.ProductName, order.Quantity, order.Price)).ToList();
        }
    }
}