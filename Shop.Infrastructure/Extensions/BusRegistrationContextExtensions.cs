using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Shop.Infrastructure.MessageBus;

namespace Shop.Infrastructure.Extensions;

public static class BusRegistrationContextExtensions
{
    public static IBusControl AddRabbitMqBus(this IBusRegistrationContext context)
    {
        return Bus.Factory.CreateUsingRabbitMq(config =>
        {
            var rabbitConfig = context.GetRequiredService<IOptions<RabbitMqConfig>>().Value;

            config.Host(new Uri(rabbitConfig.ConnectionString), hconfig =>
            {
                hconfig.Username(rabbitConfig.Username);
                hconfig.Password(rabbitConfig.Password);
            });
        });
    }
    
    public static IBusControl AddRabbitMqBus(this IBusRegistrationContext context, 
        string endpoint, 
        IEnumerable<Action<IReceiveEndpointConfigurator, IBusRegistrationContext>> consumerRetryRegistrations)
    {
        return Bus.Factory.CreateUsingRabbitMq(config =>
        {
            var rabbitConfig = context.GetRequiredService<IOptions<RabbitMqConfig>>().Value;

            config.Host(new Uri(rabbitConfig.ConnectionString), hconfig =>
            {
                hconfig.Username(rabbitConfig.Username);
                hconfig.Password(rabbitConfig.Password);
            });
            
            config.ReceiveEndpoint(endpoint, econfig =>
            {
                econfig.PrefetchCount = 16;
                econfig.UseMessageRetry(rconfig =>
                {
                    rconfig.Interval(2, 100);
                });

                foreach (var consumerRetry in consumerRetryRegistrations)
                {
                    consumerRetry.Invoke(econfig, context);
                }
            });
        });
    }
}