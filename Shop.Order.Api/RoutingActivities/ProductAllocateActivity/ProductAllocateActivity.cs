using System.Text.Json;
using MassTransit;
using MongoDB.Bson.IO;
using Shop.Infrastructure.Inventory;

namespace Shop.Infrastructure.RoutingActivities.ProductAllocateActivity;

public class ProductAllocateActivity: IActivity<Order.Order, OrderLog>
{
    public async Task<ExecutionResult> Execute(ExecuteContext<Order.Order> context)
    {
        try
        {
            var items = new List<StockProduct>();

            foreach (var item in context.Arguments.Items)
            {
                items.Add(new StockProduct(){ProductId = item.ProductId, Quantity = item.Quantity});
            }
            
            var productReleaseCommand = new ProductReleaseCommand() { Items = items };
            var endpoint = await context.GetSendEndpoint(new Uri("rabbitmq://localhost/release_product"));

            await endpoint.Send(productReleaseCommand);
            
            return context.Completed(productReleaseCommand);
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
    public Order.Order Order { get; set; } = null!;
    public string Message { get; set; } = null!;
}