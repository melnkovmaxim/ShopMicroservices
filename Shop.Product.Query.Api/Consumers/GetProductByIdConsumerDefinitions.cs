using MassTransit;

namespace Shop.Product.Query.Api.Handlers;

public class GetProductByIdConsumerDefinitions: ConsumerDefinition<GetProductByIdConsumer>
{
    protected override void ConfigureConsumer(IReceiveEndpointConfigurator endpointConfigurator, IConsumerConfigurator<GetProductByIdConsumer> consumerConfigurator)
    {
        endpointConfigurator.PrefetchCount = 16;
        endpointConfigurator.UseMessageRetry(rconfig => { rconfig.Interval(2, 100); });
        base.ConfigureConsumer(endpointConfigurator, consumerConfigurator);
    }
}