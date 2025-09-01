using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using NuGet.Protocol;
using Order.Application.DTO;
using Order.Application.Services;
using Serilog;

namespace Order.Infrastructure.Services;

    /// <summary>
    /// Service for interacting with products service.
    /// </summary>
    public class ProductService : IProductService
    {
        private readonly HttpClient _Httpclient;
        //private readonly string BaseUrl = "http://localhost:5100/Products" ;
        //using docker container
        private readonly string BaseUrl = "http://productserviceapi:8080/Products";
        private readonly ILogger<ProductService> _logger;
        //private readonly ProductGrpcClient _productGrpcClient;
        public ProductService(HttpClient httpClient, ILogger<ProductService> logger)
    {
        _Httpclient = httpClient;
        _logger = logger;
    }
        public async Task<ProductDto?> GetProduct(Guid productId)
        {
            try
            {
                string Url = $"{BaseUrl}/{productId}";

                var httpResponse = await _Httpclient.GetAsync(Url);


                if (!httpResponse.IsSuccessStatusCode)
                {
                    _logger.LogError("Failed to retrieve product with ID {ProductId}", productId);
                    return null;
                }

                var jsonResponse = await httpResponse.Content.ReadAsStringAsync();
                var productResponse = JsonSerializer.Deserialize<ProductDto>(jsonResponse);

                _logger.LogInformation(productResponse.ToJson());

                _logger.LogInformation("Successfully retrieved product with ID {ProductId}", productId);
                return productResponse;

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while logging the attempt to retrieve product with ID {ProductId}", productId);
                return null;
            }            

        }   

        public async Task<bool> ReserveProduct(UpdateProductQuantityRequest updateProductQuantityRequest){
             try
             {
                string url = $"{BaseUrl}/{updateProductQuantityRequest.ProductId}/stock";       

                string reqParams = JsonSerializer.Serialize(updateProductQuantityRequest);

                var httpContent = new StringContent(reqParams, Encoding.Default, "application/json");

                var httpResponse = await _Httpclient.PutAsync(url, httpContent);

                 await httpResponse.Content.ReadAsStringAsync();             

                if (!httpResponse.IsSuccessStatusCode)
                {
                    _logger.LogError("Failed to reserve product with ID {ProductId}", updateProductQuantityRequest.ProductId);
                    return false;
                }
                return true;
             }
             catch (System.Exception ex)
             {
                _logger.LogError("Exception occurred while reserving product with ID {ProductId}", updateProductQuantityRequest.ProductId);
               return false;                
             }   
                
         }

    
}