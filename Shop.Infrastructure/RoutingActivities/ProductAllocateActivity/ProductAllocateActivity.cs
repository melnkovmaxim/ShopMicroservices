using System.Text.Json;
using MassTransit;
using Shop.Infrastructure.Inventory;

namespace Shop.Infrastructure.RoutingActivities.AllocateProductActivity;

using Order;

public class AllocateProductActivity: IActivity<Order, OrderLog>
{
    public async Task<ExecutionResult> Execute(ExecuteContext<Order> context)
    {
        try
        {
            var endpoint = await context.GetSendEndpoint(new Uri("rabbitmq://localhost/allocate_product"));
            var command = JsonSerializer.Deserialize<ProductAllocateCommand>(context.Message.Variables["PlaceOrder"].ToString());

            await endpoint.Send(command);

            var log = new OrderLog()
            {
                Order = context.Arguments,
                Message = "Success"
            };
            
            return context.Completed(log);
        }
        catch (Exception _)
        {
            return context.Faulted();
        }
    }

    public Task<CompensationResult> Compensate(CompensateContext<OrderLog> context)
    {
        throw new NotImplementedException();
    }
}

public class OrderLog
{
    public Order Order { get; set; } = null!;
    public string Message { get; set; } = null!;
}