using MongoDB.Driver;
using MongoDB.Driver.Linq;
using Shop.Domain.Commands;
using Shop.Domain.Events;

namespace Shop.Product.Api.Repositories;

public class ProductRepository: IProductRepository
{
    private readonly IMongoDatabase _db;
    private readonly IMongoCollection<ProductCreateCommand> _collection;

    public ProductRepository(IMongoDatabase db)
    {
        _db = db;
        _collection = db.GetCollection<ProductCreateCommand>("product");
    }
    
    public async Task<ProductCreatedEvent> GetProductAsync(string productId)
    {
        var product = await _collection.AsQueryable()
            .Where(p => p.ProductId == productId)
            .FirstOrDefaultAsync();

        if (product is null)
        {
            throw new Exception($"Продукт {productId} не найден");
        }

        return new ProductCreatedEvent()
        {
            ProductId = productId,
            ProductName = product.ProductName,
            CreatedAt = DateTime.UtcNow
        };
    }

    public async Task<ProductCreatedEvent> AddProductAsync(ProductCreateCommand command)
    {
        await _collection.InsertOneAsync(command);

        return new ProductCreatedEvent()
        {
            ProductId = command.ProductId,
            ProductName = command.ProductName,
            CreatedAt = DateTime.UtcNow
        };
    }
}