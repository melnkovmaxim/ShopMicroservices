using MassTransit;

namespace Shop.Cart.Api.Consumers;

public class CartItemRemoveConsumerDefinition: ConsumerDefinition<CartItemRemoveConsumer>
{
    public CartItemRemoveConsumerDefinition()
    {
        EndpointName = "remove_cart";
    }

    protected override void ConfigureConsumer(IReceiveEndpointConfigurator endpointConfigurator, IConsumerConfigurator<CartItemRemoveConsumer> consumerConfigurator)
    {
        endpointConfigurator.PrefetchCount = 16;
        endpointConfigurator.UseMessageRetry(rconfig => { rconfig.Interval(2, 100); });
        base.ConfigureConsumer(endpointConfigurator, consumerConfigurator);
    }
}