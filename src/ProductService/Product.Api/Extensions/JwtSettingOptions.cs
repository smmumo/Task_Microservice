using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Product.Api.Extensions
{
    public class JwtSettingOptions
    {
         public string Issuer { get; set; } = string.Empty;
        public string Audience { get; set; } = string.Empty;
        public string SecurityKey { get; set; } = string.Empty;
    }
}