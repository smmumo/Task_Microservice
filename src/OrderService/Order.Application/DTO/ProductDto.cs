using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Order.Application.DTO
{    
    public class ProductDto
    {
        [JsonPropertyName("id")]
        public required string Id { get; set; } 
        [JsonPropertyName("productName")]
        public required string ProductName { get; set; } 
        [JsonPropertyName("quantity")]
        public required decimal Quantity { get; set; }
    }
}