using MassTransit;
using Shop.Infrastructure.Wallet;
using Shop.Wallet.DataProvider.Repository;

namespace Shop.Wallet.Api.Consumers;

public class FundsDeductConsumer: IConsumer<FundsDeductCommand>
{
    private readonly IWalletRepository _walletRepository;

    public FundsDeductConsumer(IWalletRepository walletRepository)
    {
        _walletRepository = walletRepository;
    }
    
    public Task Consume(ConsumeContext<FundsDeductCommand> context)
    {
        return _walletRepository.DeductFundsAsync(context.Message);
    }
}