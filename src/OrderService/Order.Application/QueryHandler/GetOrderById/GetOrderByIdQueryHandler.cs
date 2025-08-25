using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Abstractions.Messaging;
using Microsoft.EntityFrameworkCore;
using Order.Application.Data;
using Order.Application.DTO;
using Order.Domain.Entity;
using Order.Domain.Repository;

namespace Order.Application.QueryHandler
{
    public class GetOrderByIdQueryHandler : IQueryHandler<GetOrderByIdQuery, OrderDto?>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IDbContext _dbContext;

        public GetOrderByIdQueryHandler(IOrderRepository orderRepository, IDbContext dbContext)
        {
            _orderRepository = orderRepository;
            _dbContext = dbContext;
        }

        public async Task<OrderDto?> Handle(GetOrderByIdQuery query, CancellationToken cancellationToken)
        {
            var order = await _dbContext.Set<Orders>()
                 .Where(o => o.Id == query.OrderId)
                .FirstOrDefaultAsync(cancellationToken);
              
            //await _orderRepository.GetByIdAsync(query.OrderId, cancellationToken);
            return order is not null ? new OrderDto(order.Id, order.ProductName, order.Quantity, order.Price) : null;
        }
    }
}