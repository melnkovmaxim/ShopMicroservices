using Shop.Domain.Commands;
using Shop.Domain.Events;

namespace Shop.Product.DataProvider.Services;

public interface IProductService
{
    Task<ProductCreatedEvent> GetProductAsync(string productId);
    Task<ProductCreatedEvent> AddProductAsync(ProductCreateCommand command);
}