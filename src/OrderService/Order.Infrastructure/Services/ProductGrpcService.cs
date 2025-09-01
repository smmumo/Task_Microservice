using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Grpc.Core;
using Microsoft.Extensions.Logging;
using Order.Application.DTO;
using Order.Application.Services;
using ProductApiClient;

namespace Order.Infrastructure.Services
{
    public class ProductGrpcService : IProductGrpcService
    {
        private readonly ILogger<ProductGrpcService> _logger;
        private readonly ProductGrpc.ProductGrpcClient _client;

        public ProductGrpcService(ILogger<ProductGrpcService> logger, ProductGrpc.ProductGrpcClient client)
        {
            _logger = logger;
            _client = client;
        }

        public async Task<ProductDto?> GetProduct(Guid productId)
        {
            try
            {
                var productResponse = await _client.GetProductAsync(new GetProductRequest { Id = productId.ToString() });
                if (productResponse is null) return null;

                _logger.LogInformation("Product with id: {ProductId} retrieved successfully", productId);
                return new ProductDto
                {
                    Id = productResponse.Id,
                    ProductName = productResponse.Name,
                    Quantity = (decimal)productResponse.Quantity
                };
            }
            catch (RpcException e)
            {
                _logger.LogWarning(e, "ERROR - Parameters: {@parameters}", new { ProductId = productId });

                _logger.LogError("Error occurred while trying to get product with id: {ProductId}", productId);
                
                return null;
            }
           
        }

        public async Task<bool> ReserveProduct(UpdateProductQuantityRequest updateProductQuantityRequest)
        {
           try
           {
                await _client.ReserveProductAsync( new ReserveProductRequest
                {
                    Id = updateProductQuantityRequest.ProductId.ToString(),
                    Quantity = (double)updateProductQuantityRequest.Quantity
                });
                //await Task.Delay(10); // Simulate async work
                _logger.LogInformation("Product with id: {ProductId} reserved successfully", updateProductQuantityRequest.ProductId);
                return true;
           }
           catch (RpcException e)
           {
                _logger.LogWarning(e, "ERROR - Parameters: {@parameters}", new { ProductId = updateProductQuantityRequest.ProductId, Quantity = updateProductQuantityRequest.Quantity });

                _logger.LogError("Error occurred while trying to reserve product with id: {ProductId}", updateProductQuantityRequest.ProductId);
                
                return false;
           }
        }
    }
}