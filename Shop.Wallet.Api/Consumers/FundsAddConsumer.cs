using MassTransit;
using Shop.Infrastructure.Wallet;
using Shop.Wallet.DataProvider.Repository;

namespace Shop.Wallet.Api.Consumers;

public class FundsAddConsumer: IConsumer<FundsAddCommand>
{
    private readonly IWalletRepository _walletRepository;

    public FundsAddConsumer(IWalletRepository walletRepository)
    {
        _walletRepository = walletRepository;
    }
    
    public Task Consume(ConsumeContext<FundsAddCommand> context)
    {
        return _walletRepository.AddFundsAsync(context.Message);
    }
}