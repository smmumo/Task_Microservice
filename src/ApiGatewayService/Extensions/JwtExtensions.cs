using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace ApiGatewayService.Extensions;

    public static class JwtExtensions
    {
    public static IServiceCollection AddJwtAuthentication(this IServiceCollection services, IConfiguration iconfiguration)
    {
        var jwtOptions = iconfiguration.GetSection("JwtSettings").Get<JwtSettingOptions>();
        string SecurityKey = jwtOptions.SecurityKey;

        services.AddAuthentication(opt =>
        {
            opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = false,
                ValidIssuer = jwtOptions.Issuer,
                ValidateAudience = false,
                ValidateIssuerSigningKey = true,
                //ValidAudience = jwtOptions.Audience,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SecurityKey))
            };
        });  
        return services;        
    }
}
