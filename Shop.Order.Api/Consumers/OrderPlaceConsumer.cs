using MassTransit;
using MassTransit.Courier.Contracts;
using Shop.Infrastructure.Cart;
using Shop.Infrastructure.RoutingActivities.CartUpdateActivity;
using Shop.Infrastructure.RoutingActivities.ProductAllocateActivity;
using Shop.Infrastructure.RoutingActivities.UpdateOrderActivity;
using Shop.Infrastructure.RoutingActivities.WalletActivity;

namespace Shop.Order.Api.Consumers;

using Infrastructure.Order;

public class OrderPlaceConsumer: IConsumer<Order>
{
    
    private IEndpointNameFormatter _endpointNameFormatter;
    public OrderPlaceConsumer(IEndpointNameFormatter endpointNameFormatter)
    {
        _endpointNameFormatter = endpointNameFormatter;
    }
    
    public Task Consume(ConsumeContext<Order> context)
    {
        try
        {
            var slip = CreateRoutingSlip(context);

            return context.Execute(slip);
        }
        catch (Exception)
        {
            return Task.CompletedTask;
        }
    }

    private RoutingSlip CreateRoutingSlip(ConsumeContext<Order> context)
    {
        var order = context.Message;
        var builder = new RoutingSlipBuilder(new Guid(order.OrderId));
        
        builder.AddVariable("RequestId", context.RequestId);
        builder.AddVariable("ResponseAddress", context.ResponseAddress);
        builder.AddVariable("PlacedOrder", order);
        
        var walletActivityQueueName = _endpointNameFormatter.ExecuteActivity<WalletActivity, MoneyTransact>();
        builder.AddActivity("PROCCESS WALLET", new Uri($"rabbitmq://localhost/{walletActivityQueueName}"),
            new { UserId = order.UserId, Amount = order.Amount,  });
        
        var productActivityQueueName = _endpointNameFormatter.ExecuteActivity<ProductAllocateActivity, Order>();
        builder.AddActivity("ALLOCATE PRODUCT", new Uri($"rabbitmq://localhost/{productActivityQueueName}"), order);
        
        var orderUpdateQueueName = _endpointNameFormatter.ExecuteActivity<OrderUpdateActivity, Order>();
        builder.AddActivity("UPDATE_ORDER", new Uri($"rabbitmq://localhost/{orderUpdateQueueName}"), order);
        
        var cartUpdateQueueName = _endpointNameFormatter.ExecuteActivity<CartUpdateActivity, Cart>();
        builder.AddActivity("UPDATE_ORDER", new Uri($"rabbitmq://localhost/{cartUpdateQueueName}"), 
            new { order.UserId });

        return builder.Build();
    }
}