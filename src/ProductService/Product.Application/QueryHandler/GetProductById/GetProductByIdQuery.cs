using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Product.Application.Abstractions.Messaging;
using Product.Application.Application.DTO;

namespace Product.Application.QueryHandler
{
    public record GetProductByIdQuery(Guid ProductId) : IQuery<ProductDto?>;
}