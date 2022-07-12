using MassTransit;
using Shop.Infrastructure.Wallet;

namespace Shop.Wallet.Api.Consumers;

public class FundsAddConsumer: IConsumer<FundsAddCommand>
{
    public Task Consume(ConsumeContext<FundsAddCommand> context)
    {
        throw new NotImplementedException();
    }
}