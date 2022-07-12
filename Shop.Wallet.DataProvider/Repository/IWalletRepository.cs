using Shop.Infrastructure.Wallet;

namespace Shop.Wallet.DataProvider.Repository;

using Infrastructure.Wallet;

public interface IWalletRepository
{
    Task AddFundsAsync(FundsAddCommand command);
    Task DeductFundsAsync(DeductFundsCommand command);
}