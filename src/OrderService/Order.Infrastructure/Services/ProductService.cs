using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;
using Order.Application.DTO;
using Order.Application.Services;
using Serilog;

namespace Order.Infrastructure.Services
{
    public class ProductService : IProductService
    {
         private readonly HttpClient _Httpclient ;
         //private readonly string BaseUrl = "http://localhost:5100/ " ;
        //using docker container
        private readonly string BaseUrl = "http://product.services.api:6000/ " ;
        public ProductService(HttpClient httpClient)
        {
            _Httpclient = httpClient;
        }
        public async Task<ProductDto?> GetProduct(Guid productId)
        {
            string Url = $"{BaseUrl}products/{productId}";

            var httpResponse = await _Httpclient.GetAsync(Url);           

            using var log = new LoggerConfiguration().WriteTo.Console().CreateLogger();

            if (!httpResponse.IsSuccessStatusCode)
            {
                log.Information("Failed to retrieve product with ID {ProductId}", productId);
                return null;
            }

            var jsonResponse = await httpResponse.Content.ReadAsStringAsync();
            var productResponse = JsonSerializer.Deserialize<ProductDto>(jsonResponse);
            log.Information("Successfully retrieved product with ID {ProductId}", productId);
            return productResponse;

            
        }
    }
}