using MassTransit;
using Shop.Infrastructure.Wallet;

namespace Shop.Wallet.Api.Consumers;

public class FundsDeductConsumer: IConsumer<DeductFundsCommand>
{
    public Task Consume(ConsumeContext<DeductFundsCommand> context)
    {
        throw new NotImplementedException();
    }
}