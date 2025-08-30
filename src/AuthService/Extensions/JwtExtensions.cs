using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuthService.Contracts;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace AuthService.Extensions;

    public static class JwtExtensions
    {
        public static void AddJwtAuthentication(this IServiceCollection services, IConfiguration iconfiguration)
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
                    ValidateIssuer = true,
                    ValidIssuer = jwtOptions.Issuer,
                    ValidateAudience = false,
                    ValidateIssuerSigningKey = true,
                    //ValidAudience = jwtOptions.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SecurityKey))
                };
            });           
        

        }
}
