using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Abstractions.Messaging;
using Order.Domain.Core.Results;

namespace Order.Application.CommandHandler.CreateOrder
{
    public record CreateOrderCommand(
        Guid ProductId,
        //string ProductName,
        decimal TotalAmount,
        decimal Qty) : ICommand<Result>;
}