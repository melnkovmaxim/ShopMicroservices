using Shop.Infrastructure.Inventory;

namespace Shop.Inventory.DataProvider.Repositories;

public interface IStockRepository
{
    Task AllocateProductsAsync(ProductAllocateCommand command);
    Task ReleaseProductsAsync(ProductReleaseCommand command);
}