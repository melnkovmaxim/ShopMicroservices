using MassTransit;
using Shop.Infrastructure.Wallet;

namespace Shop.Infrastructure.RoutingActivities.WalletActivity;

public class WalletActivity: IActivity<MoneyTransact, MoneyTransactLog>
{
    public async Task<ExecutionResult> Execute(ExecuteContext<MoneyTransact> context)
    {
        try
        {
            var deductFunds = new FundsDeductCommand()
            {
                DebitAmount = context.Arguments.Amount,
                UserId = context.Arguments.UserId
            };
            var endpoint = await context.GetSendEndpoint(new Uri("rabbitmq://localhost/deduct_funds"));

            await endpoint.Send(deductFunds);

            var log = new MoneyTransactLog()
            {
                Amount = context.Arguments.Amount,
                UserId = context.Arguments.UserId
            };

            return context.Completed(log);
        }
        catch (Exception _)
        {
            return context.Faulted();
        }
    }

    public Task<CompensationResult> Compensate(CompensateContext<MoneyTransactLog> context)
    {
        throw new NotImplementedException();
    }
}

public class MoneyTransact
{
    public string UserId { get; set; } = null!;
    public decimal Amount { get; set; }
}

public class MoneyTransactLog
{
    public string UserId { get; set; } = null!;
    public decimal Amount { get; set; }
    public string Message { get; set; } = null!;
}