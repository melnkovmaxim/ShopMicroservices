using MassTransit;
using MassTransit.Courier.Contracts;

namespace Shop.Order.Api.Consumers;

using Infrastructure.Order;

public class OrderPlaceConsumer: IConsumer<Order>
{
    public Task Consume(ConsumeContext<Order> context)
    {
        throw new NotImplementedException();
    }

    private RoutingSlip CreateRoutingSlip(Order order, ConsumeContext<Order> context)
    {
        var builder = new RoutingSlipBuilder(new Guid(order.OrderId));
    }
}