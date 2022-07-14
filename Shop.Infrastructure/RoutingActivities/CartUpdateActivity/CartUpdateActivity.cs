using MassTransit;

namespace Shop.Infrastructure.RoutingActivities.CartUpdateActivity;

using Cart;

public class CartUpdateActivity : IExecuteActivity<Cart>
{
    public async Task<ExecutionResult> Execute(ExecuteContext<Cart> context)
    {
        var endpoint = await context.GetSendEndpoint(new Uri("rabbitmq://localhost/remove_cart"));

        await endpoint.Send(context.Arguments);

        return context.Completed();
    }
}