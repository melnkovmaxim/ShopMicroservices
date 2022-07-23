using MassTransit;
using Shop.Infrastructure.Order;
using Shop.Order.DataProvider.Repositories;

namespace Shop.Order.Api.Consumers;
using Infrastructure.Order;

public class OrderCreateConsumer: IConsumer<OrderCreateCommand>
{
    private readonly IOrderRepository _orderRepository;

    public OrderCreateConsumer(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }
    
    public Task Consume(ConsumeContext<OrderCreateCommand> context)
    {
        var order = new Order()
        {
            Items = context.Message.Items,
            OrderId = context.Message.OrderId,
            UserId = context.Message.UserId
        };

        return _orderRepository.CreateOrderAsync(order);
    }
}