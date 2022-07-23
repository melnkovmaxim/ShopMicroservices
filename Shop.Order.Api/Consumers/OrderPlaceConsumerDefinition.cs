using MassTransit;

namespace Shop.Order.Api.Consumers;

public class OrderPlaceConsumerDefinition: ConsumerDefinition<OrderPlaceConsumer>
{
    public OrderPlaceConsumerDefinition()
    {
        EndpointName = "place_order";
    }
}