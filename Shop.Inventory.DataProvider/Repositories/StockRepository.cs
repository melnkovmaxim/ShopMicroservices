using MongoDB.Driver;
using MongoDB.Driver.Linq;
using Shop.Infrastructure.Inventory;

namespace Shop.Inventory.DataProvider.Repositories;

public class StockRepository : IStockRepository
{
    private readonly IMongoClient _mongoClient;
    private readonly IMongoCollection<StockProduct> _productCollection;

    public StockRepository(IMongoDatabase mongo, IMongoClient mongoClient)
    {
        _mongoClient = mongoClient;
        _productCollection = mongo.GetCollection<StockProduct>(nameof(StockProduct));
    }

    public async Task AllocateProductsAsync(ProductAllocateCommand command)
    {
        var session = await _mongoClient.StartSessionAsync();

        session.StartTransaction();

        try
        {
            foreach (var item in command.Products)
            {
                var product = await GetProductByIdAsync(item.ProductId);

                product.Quantity += item.Quantity;

                await _productCollection.ReplaceOneAsync(x => x.ProductId == item.ProductId, product);
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
        var session = await _mongoClient.StartSessionAsync();

        session.StartTransaction();

        try
        {
            foreach (var item in command.Products)
            {
                var product = await GetProductByIdAsync(item.ProductId);

                product.Quantity -= item.Quantity;

                await _productCollection.ReplaceOneAsync(x => x.ProductId == item.ProductId, product);
            }

            await session.CommitTransactionAsync();
        }
        finally
        {
            await session.AbortTransactionAsync();
        }
    }

    private async Task<StockProduct> GetProductByIdAsync(string productId)
    {
        var existedProduct = await _productCollection.AsQueryable().FirstOrDefaultAsync(x => x.ProductId == productId);

        return existedProduct ?? new StockProduct();
    }
}