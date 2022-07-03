using MassTransit;

namespace Shop.Cart.Api.Consumers;

public class CartItemAddConsumerDefinition: ConsumerDefinition<CartItemAddConsumer>
{
    public CartItemAddConsumerDefinition()
    {
        EndpointName = "add_cart";
    }

    protected override void ConfigureConsumer(IReceiveEndpointConfigurator endpointConfigurator, IConsumerConfigurator<CartItemAddConsumer> consumerConfigurator)
    {
        endpointConfigurator.PrefetchCount = 16;
        endpointConfigurator.UseMessageRetry(rconfig => { rconfig.Interval(2, 100); });
        base.ConfigureConsumer(endpointConfigurator, consumerConfigurator);
    }
}