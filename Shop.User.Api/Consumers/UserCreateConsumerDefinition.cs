using MassTransit;

namespace Shop.User.Api.Consumers;

public class UserCreateConsumerDefinition: ConsumerDefinition<UserCreateConsumer>
{
    public UserCreateConsumerDefinition()
    {
        EndpointName = "add_user";
    }

    protected override void ConfigureConsumer(IReceiveEndpointConfigurator endpointConfigurator, IConsumerConfigurator<UserCreateConsumer> consumerConfigurator)
    {
        endpointConfigurator.PrefetchCount = 16;
        endpointConfigurator.UseMessageRetry(rconfig => { rconfig.Interval(2, 100); });
        base.ConfigureConsumer(endpointConfigurator, consumerConfigurator);
    }
}