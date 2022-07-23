using MassTransit;

namespace Shop.Order.Api.Consumers;

public class OrderCreateConsumerDefinition: ConsumerDefinition<OrderCreateConsumer>
{
    public OrderCreateConsumerDefinition()
    {
        EndpointName = "create_order";
    }
}