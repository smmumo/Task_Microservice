

using Application.Abstractions.Messaging;
using Microsoft.Extensions.DependencyInjection;

namespace Order.Application
{
    public static class DependencyInjection
    {
        /// <summary>
        /// Registers the necessary services with the DI framework.
        /// </summary>
        /// <param name="services">The service collection.</param>
        /// <returns>The same service collection.</returns>
        public static IServiceCollection AddOrderApplication(this IServiceCollection services)
        {
            // services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

            // services.AddMediatR(cfg =>
            // {
            //     cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());

            //    // cfg.AddOpenBehavior(typeof(ValidationBehaviour<,>));

            //    // cfg.AddOpenBehavior(typeof(TransactionBehaviour<,>));
            // });
            
             services.Scan(scan => scan.FromAssembliesOf(typeof(DependencyInjection))
            .AddClasses(classes => classes.AssignableTo(typeof(IQueryHandler<,>)))
            .AsImplementedInterfaces()
            .WithScopedLifetime()
            .AddClasses(classes => classes.AssignableTo(typeof(ICommandHandler<>)))
            .AsImplementedInterfaces()
            .WithScopedLifetime()
            .AddClasses(classes => classes.AssignableTo(typeof(ICommandHandler<,>)))
            .AsImplementedInterfaces()
            .WithScopedLifetime());
          
          //domain events
            //   services.Scan(scan => scan.FromAssembliesOf(typeof(DependencyInjection))
            //     .AddClasses(classes => classes.AssignableTo(typeof(IDomainEventHandler<>)))
            //     .AsImplementedInterfaces()
            //     .WithScopedLifetime()
            //     );

            return services;
        }
    }
}
