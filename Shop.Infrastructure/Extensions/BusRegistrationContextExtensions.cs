using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Shop.Infrastructure.MessageBus;

namespace Shop.Infrastructure.Extensions;

public static class BusRegistrationContextExtensions
{
    public static IBusControl AddRabbitMqBus(this IBusRegistrationContext context, bool isEndpoint = false)
    {
        return Bus.Factory.CreateUsingRabbitMq(config =>
        {
            var rabbitConfig = context.GetRequiredService<IOptions<RabbitMqConfig>>().Value;

            config.Host(new Uri(rabbitConfig.ConnectionString), hconfig =>
            {
                hconfig.Username(rabbitConfig.Username);
                hconfig.Password(rabbitConfig.Password);
            });
            
            
            config.ConfigureEndpoints(context);
        });
    }

    public static IBusControl AddRabbitMqBus(this IBusRegistrationContext context,
        IEnumerable<Action<IReceiveEndpointConfigurator, IBusRegistrationContext>> consumerRetryRegistrations,
        string? endpoint = null)
    {
        return Bus.Factory.CreateUsingRabbitMq(config =>
        {
            var rabbitConfig = context.GetRequiredService<IOptions<RabbitMqConfig>>().Value;

            config.Host(new Uri(rabbitConfig.ConnectionString), hconfig =>
            {
                hconfig.Username(rabbitConfig.Username);
                hconfig.Password(rabbitConfig.Password);
            });

            if (endpoint is null)
            {
                config.ConfigureEndpoints(context);
            }
            else
            {
                config.ReceiveEndpoint(endpoint, econfig =>
                {
                    econfig.PrefetchCount = 16;
                    econfig.UseMessageRetry(rconfig => { rconfig.Interval(2, 100); });

                    foreach (var consumerRetry in consumerRetryRegistrations)
                    {
                        consumerRetry.Invoke(econfig, context);
                    }
                });
            }
            
        });
    }
}