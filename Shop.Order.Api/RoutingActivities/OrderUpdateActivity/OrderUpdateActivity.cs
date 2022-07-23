using MassTransit;

namespace Shop.Infrastructure.RoutingActivities.UpdateOrderActivity;

using Order;

public class OrderUpdateActivity: IActivity<Order, OrderUpdateLog>
{
    public async Task<ExecutionResult> Execute(ExecuteContext<Order> context)
    {
        try
        {
            var endpoint = await context.GetSendEndpoint(new Uri("rabbitmq://localhost/create_order_consumer"));
            var order = new OrderCreateCommand()
            {
                Items = context.Arguments.Items,
                OrderId = context.Arguments.OrderId,
                UserId = context.Arguments.UserId
            };
            
            await endpoint.Send(order);

            var log = new OrderUpdateLog()
            {
                OrderId = context.Arguments.OrderId
            };
            
            return context.Completed(log);
        }
        catch(Exception _)
        {
            return context.Faulted();
        }
    }

    public Task<CompensationResult> Compensate(CompensateContext<OrderUpdateLog> context)
    {
        throw new NotImplementedException();
    }
}

public class OrderUpdateLog
{
    public string OrderId { get; set; } = null!;
}