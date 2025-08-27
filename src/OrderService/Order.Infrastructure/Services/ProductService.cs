using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
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
        private readonly string BaseUrl = "http://product.services.api:6000/Products/";
        public ProductService(HttpClient httpClient)
        {
            _Httpclient = httpClient;
        }
        public async Task<ProductDto?> GetProduct(Guid productId)
        {
            string Url = $"{BaseUrl}/{productId}";

            var httpResponse = await _Httpclient.GetAsync(Url);

            using var log = new LoggerConfiguration().WriteTo.Console().CreateLogger();

            if (!httpResponse.IsSuccessStatusCode)
            {
                log.Information("Failed to retrieve product with ID {ProductId}", productId);
                return null;
            }

            var jsonResponse = await httpResponse.Content.ReadAsStringAsync();
            var productResponse = JsonSerializer.Deserialize<ProductDto>(jsonResponse);

            log.Information(productResponse.ToJson());

            log.Information("Successfully retrieved product with ID {ProductId}", productId);
            return productResponse;


        }   

        public async Task<bool> ReserveProduct(UpdateProductQuantityRequest updateProductQuantityRequest){

                string url = $"{BaseUrl}/{updateProductQuantityRequest.ProductId}/stock";       

                string reqParams = JsonSerializer.Serialize(updateProductQuantityRequest);

                var httpContent = new StringContent(reqParams, Encoding.Default, "application/json");

                var httpResponse = await _Httpclient.PutAsync(url, httpContent);

                 await httpResponse.Content.ReadAsStringAsync();              

                 using var log = new LoggerConfiguration().WriteTo.Console().CreateLogger();

                if (!httpResponse.IsSuccessStatusCode)
                {
                    log.Information("Failed to reserve product with ID {ProductId}", updateProductQuantityRequest.ProductId);
                    return false;
                }
                return true;
         }

    
}