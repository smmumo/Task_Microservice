
using System.Text;
using Application.Abstractions.Messaging;
using Infrastructure.Common;
using Infrastructure.Messaging;
using Infrastructure.Persistence;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Order.Application;
using Order.Application.CommandHandler.CreateOrder;
using Order.Application.Data;
using Order.Application.DTO;
using Order.Application.Messaging;
using Order.Application.QueryHandler;
using Order.Application.Services;
using Order.Domain.Core.Results;
using Order.Domain.Repository;
using Order.Infrastructure.Messaging;
using Order.Infrastructure.Repository;
using Order.Infrastructure.Services;
using Polly;
using Polly.Extensions.Http;


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

        services.AddScoped<IDbContext>(serviceProvider => serviceProvider.GetRequiredService<OrderDbContext>());
        services.AddScoped<IUnitOfWork>(serviceProvider => serviceProvider.GetRequiredService<OrderDbContext>());

        services.AddDbContext<OrderDbContext>(
            options => options.UseInMemoryDatabase("OrderDb"));


        services.AddScoped<IQueryDispatcher, QueryDispatcher>();
        services.AddScoped<ICommandDispatcher, CommandDispatcher>();
        services.AddScoped<IOrderRepository, OrderRepository>();

        services.AddScoped<ICommandHandler<CreateOrderCommand, Result>, CreateOrderCommandHandler>();
        services.AddScoped<IQueryHandler<GetOrderByIdQuery, OrderDto?>, GetOrderByIdQueryHandler>();
        services.AddScoped<IQueryHandler<GetOrdersQuery, List<OrderDto>>, GetOrdersQueryHandler>();       
        
        //var retryPolicy = GetRetryPolicy();
        //var circuitBreakerPolicy = GetCircuitBreakerPolicy();

        services.AddHttpClient<IProductService, ProductService>() ;

        // services.AddHttpClient<IProductService, ProductService>()
        //     .SetHandlerLifetime(TimeSpan.FromMinutes(5))  
        //     .AddPolicyHandler(retryPolicy)
        //     .AddPolicyHandler(circuitBreakerPolicy);

        //services.AddSingleton<IIntegrationEventPublisher, IntegrationEventPublisher>();

        return services;
    }

   
    // Todo: move to separate file
    static IAsyncPolicy<HttpResponseMessage> GetRetryPolicy()
    {
        return HttpPolicyExtensions
            .HandleTransientHttpError()
            .OrResult(msg => msg.StatusCode == System.Net.HttpStatusCode.NotFound)
            .WaitAndRetryAsync(3, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)));
    }
        
    // Todo: move to separate file
    static IAsyncPolicy<HttpResponseMessage> GetCircuitBreakerPolicy()
    {
        return HttpPolicyExtensions
            .HandleTransientHttpError()
            .CircuitBreakerAsync(2, TimeSpan.FromSeconds(30));
    }
    }