using Shop.Domain.Commands;
using Shop.Domain.Events;

namespace Shop.Product.Api.Repositories;

public interface IProductRepository
{
    Task<ProductCreatedEvent> GetProductAsync(string productId);
    Task<ProductCreatedEvent> AddProductAsync(ProductCreateCommand command);
}