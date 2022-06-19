using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Shop.Infrastructure.Options;

namespace Shop.Infrastructure.Extensions;

public static class BusRegistrationContextExtensions
{

    public static IBusControl AddRabbitMqBus(this IBusRegistrationContext context)
    {
        return Bus.Factory.CreateUsingRabbitMq(config =>
        {
            var rabbitConfig = context.GetRequiredService<IOptions<RabbitMqOptions>>().Value;

            config.Host(new Uri(rabbitConfig.ConnectionString), hconfig =>
            {
                hconfig.Username(rabbitConfig.Username);
                hconfig.Password(rabbitConfig.Password);
            });
            
        });
    }
}