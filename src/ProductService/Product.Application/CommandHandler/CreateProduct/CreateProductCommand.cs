using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Product.Application.Abstractions.Messaging;
using Product.Domain.Core;

namespace Product.Application.CommandHandler.CreateProduct
{
    public record CreateProductCommand(      
        string ProductName,     
        decimal Quantity) : ICommand<Result>;
}