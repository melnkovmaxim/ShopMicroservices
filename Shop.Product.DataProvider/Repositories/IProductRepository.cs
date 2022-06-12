using Shop.Domain.Commands;
using Shop.Domain.Events;

namespace Shop.Product.DataProvider.Repositories;

public interface IProductRepository
{
    Task<ProductCreatedEvent> GetProductAsync(string productId);
    Task<ProductCreatedEvent> AddProductAsync(ProductCreateCommand command);
}