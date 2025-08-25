
using System.Text;

using Infrastructure.Common;
using Infrastructure.Messaging;
using Infrastructure.Persistence;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Product.Application.Abstractions.Messaging;
using Product.Application.Application.Data;
using Product.Application.Application.DTO;
using Product.Application.CommandHandler.CreateProduct;
using Product.Application.CommandHandler.UpdateProduct;
using Product.Application.QueryHandler;
using Product.Domain.Core;
using Product.Domain.Repository;
using Product.Infrastructure.Repository;



namespace Infrastructure;

    public static class DependencyInjection
    {
    /// <summary>
    /// Registers the necessary services with the DI framework.
    /// </summary>
    /// <param name="services">The service collection.</param>
    /// <param name="configuration">The configuration.</param>
    /// <returns>The same service collection.</returns>
    public static IServiceCollection AddInfrastructure(this IServiceCollection services,
                IConfiguration configuration)
    {

        //services.AddOrderApplication();
        
        services.AddScoped<IDbContext>(serviceProvider => serviceProvider.GetRequiredService<ProductDbContext>());
        services.AddScoped<IUnitOfWork>(serviceProvider => serviceProvider.GetRequiredService<ProductDbContext>());

        services.AddDbContext<ProductDbContext>(
            options => options.UseInMemoryDatabase("ProductDb"));
          

        services.AddScoped<IQueryDispatcher, QueryDispatcher>();
        services.AddScoped<ICommandDispatcher, CommandDispatcher>();

        services.AddScoped<IProductRepository, ProductRepository>();

        services.AddScoped<ICommandHandler<CreateProductCommand, Result>, CreateProductCommandHandler>();
        services.AddScoped<ICommandHandler<UpdateStockQuantityCommand, Result>, UpdateStockQuantityCommandHandler>();
        services.AddScoped<IQueryHandler<GetProductByIdQuery, ProductDto?>, GetProductByIdQueryHandler>();
        services.AddScoped<IQueryHandler<GetProductsQuery, List<ProductDto>>, GetProductQueryHandler>();

        return services;
    }
    }