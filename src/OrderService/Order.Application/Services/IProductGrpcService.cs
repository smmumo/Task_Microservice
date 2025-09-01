using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Order.Application.DTO;

namespace Order.Application.Services
{
    public interface IProductGrpcService
    {
         Task<ProductDto?> GetProduct(Guid productId);
         Task<bool> ReserveProduct(UpdateProductQuantityRequest updateProductQuantityRequest);
    }
}