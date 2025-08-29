
using System.Text;
using AuthService.Domain.Repository;
using AuthService.Infrastructure;
using AuthService.Infrastructure.Persistence;
using AuthService.Infrastructure.Repository;
using AuthService.Infrastructure.Services;
using AuthService.Interface;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace AuthService.Extensions;

    public static class DependencyInjection
    {
    /// <summary>
    /// Registers the necessary services with the DI framework.
    /// </summary>
    /// <param name="services">The service collection.</param>
    /// <param name="configuration">The configuration.</param>
    /// <returns>The same service collection.</returns>
    public static IServiceCollection AuthDependencyInjection(this IServiceCollection services,
                IConfiguration configuration)
    {      
        
        services.AddScoped<IUnitOfWork>(serviceProvider => serviceProvider.GetRequiredService<AuthDbContext>());

        services.AddDbContext<AuthDbContext>(
            options => options.UseInMemoryDatabase("AuthDb"));         


       services.AddScoped<IUserRepository, UserRepository>();

        services.AddScoped<IPasswordHashChecker, PasswordHasher>();
        services.AddScoped<IPasswordHashChecker, PasswordHasher>();

        services.AddScoped<IUserRepository, UserRepository>();       

        services.AddScoped<IJwtProvider, JwtService>();
        services.AddScoped<IAuthenticationService, AuthenticationService>();

        return services;
    }
    }