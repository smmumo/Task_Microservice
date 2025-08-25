using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Abstractions.Messaging;
using Order.Application.DTO;

namespace Order.Application.QueryHandler
{
    public record GetOrdersQuery : IQuery<List<OrderDto>>;
}