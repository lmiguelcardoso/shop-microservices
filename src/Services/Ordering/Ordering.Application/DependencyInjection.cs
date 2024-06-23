using BuildingBlocks.Behaviors;
using BuildingBlocks.Messaging.MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.FeatureManagement;
using System.Reflection;


namespace Ordering.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddAplicationServices(this IServiceCollection services, IConfiguration builder)
        {
            services.AddMediatR(config =>
            {
                config.AddOpenBehavior(typeof(LoggingBehavior<,>));
                config.AddOpenBehavior(typeof(ValidationBehavior<,>));
                config.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
            });

            services.AddMessageBroker(builder, Assembly.GetExecutingAssembly());

            services.AddFeatureManagement();

            return services;
        }
    }
}
