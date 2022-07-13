using MassTransit;
using Shop.Infrastructure.Inventory;
using Shop.Inventory.DataProvider.Repositories;

namespace Shop.Inventory.Api.Consumers;

public class ProductReleaseConsumer: IConsumer<ProductReleaseCommand>
{
    private readonly IStockRepository _stockRepository;

    public ProductReleaseConsumer(IStockRepository stockRepository)
    {
        _stockRepository = stockRepository;
    }
    
    public Task Consume(ConsumeContext<ProductReleaseCommand> context)
    {
        return _stockRepository.ReleaseProductsAsync(context.Message);
    }
}