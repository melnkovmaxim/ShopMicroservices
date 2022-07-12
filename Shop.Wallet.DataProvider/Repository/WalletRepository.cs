using MongoDB.Driver;
using MongoDB.Driver.Linq;
using Shop.Infrastructure.Wallet;

namespace Shop.Wallet.DataProvider.Repository;

using Infrastructure.Wallet;

public class WalletRepository: IWalletRepository
{
    private readonly IMongoCollection<Wallet> _walletCollection;

    public WalletRepository(IMongoDatabase mongoDatabase)
    {
        _walletCollection = mongoDatabase.GetCollection<Wallet>("wallet");
    }
    
    public async Task AddFundsAsync(FundsAddCommand command)
    {
        // var existedWallet = await GetWalletAsync(command.UserId);
        //
        // if (existedWallet is null)
        // {
        //     var wallet = new Wallet()
        //     {
        //         UserId = command.UserId,
        //         Amount = command.CreditAmount
        //     };
        //     
        //     await _walletCollection.UpdateOneAsync(wallet);
        //     
        //     return 
        // }
        //
        // if (existedWallet is not null)
        // {
        //     existedWallet.Amount += command.CreditAmount;
        //     
        //     return _walletCollection
        // }
        
        
    }

    public Task DeductFundsAsync(DeductFundsCommand command)
    {
        throw new NotImplementedException();
    }

    private Task<Wallet?> GetWalletAsync(string userId)
    {
        return _walletCollection.AsQueryable().FirstOrDefaultAsync(x => x.UserId == userId);
    }
}