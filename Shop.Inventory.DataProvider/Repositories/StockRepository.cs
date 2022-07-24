using MongoDB.Driver;
using MongoDB.Driver.Linq;
using Shop.Infrastructure.Inventory;
using Shop.Infrastructure.Repositories;

namespace Shop.Inventory.DataProvider.Repositories;

public class StockRepository : RepositoryBase<StockProduct>, IStockRepository
{
    private readonly IMongoClient _mongoClient;

    public StockRepository(IMongoDatabase mongo, IMongoClient mongoClient)
        :base(mongo.GetCollection<StockProduct>(nameof(StockProduct)))
    {
        _mongoClient = mongoClient;
    }

    public async Task AllocateProductsAsync(ProductAllocateCommand command)
    {
        var session = await _mongoClient.StartSessionAsync();

        session.StartTransaction();

        try
        {
            foreach (var item in command.Products)
            {
                var product = await GetProductByIdAsync(item.Id);

                product.Quantity += item.Quantity;

                await _collection.ReplaceOneAsync(x => x.Id == item.Id, product);
            }

            await session.CommitTransactionAsync();
        }
        finally
        {
            await session.AbortTransactionAsync();
        }
    }

    public async Task ReleaseProductsAsync(ProductReleaseCommand command)
    {
        var productsForUpdate = new List<StockProduct>();
        
        foreach (var item in command.Items)
        {
            var product = await GetProductByIdAsync(item.Id);

            product.Quantity -= item.Quantity;

            productsForUpdate.Add(product);
        }
        
        await UpdateManyAsync(productsForUpdate);
    }

    private async Task<StockProduct> GetProductByIdAsync(string productId)
    {
        var existedProduct = await _collection.AsQueryable().FirstOrDefaultAsync(x => x.Id == productId);

        return existedProduct ?? new StockProduct();
    }
}