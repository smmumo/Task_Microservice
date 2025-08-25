using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Product.Application.Abstractions.Messaging;
using Product.Domain.Core;

namespace Product.Application.CommandHandler.UpdateProduct
{
    public record UpdateStockQuantityCommand(Guid ProductId, int Quantity) : ICommand<Result>;
}