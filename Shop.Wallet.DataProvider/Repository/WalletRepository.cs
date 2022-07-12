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
        var existedWallet = await GetWalletAsync(command.UserId);
        
        if (existedWallet is null)
        {
            var wallet = new Wallet()
            {
                UserId = command.UserId,
                Amount = command.CreditAmount
            };

            await _walletCollection.InsertOneAsync(wallet);

            return;
        }
        
        existedWallet.Amount += command.CreditAmount;

        await _walletCollection.ReplaceOneAsync(w => w.UserId == existedWallet.UserId, existedWallet);
    }

    public async Task DeductFundsAsync(FundsDeductCommand command)
    {
        var existedWallet = await GetWalletAsync(command.UserId);
        
        if (existedWallet is null)
        {
            var wallet = new Wallet()
            {
                UserId = command.UserId,
                Amount = -command.DebitAmount
            };

            await _walletCollection.InsertOneAsync(wallet);

            return;
        }
        
        existedWallet.Amount -= command.DebitAmount;

        await _walletCollection.ReplaceOneAsync(w => w.UserId == existedWallet.UserId, existedWallet);
    }

    private Task<Wallet?> GetWalletAsync(string userId)
    {
        return _walletCollection.AsQueryable().FirstOrDefaultAsync(x => x.UserId == userId)!;
    }
}