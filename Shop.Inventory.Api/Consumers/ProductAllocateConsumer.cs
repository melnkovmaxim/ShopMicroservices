using MassTransit;
using Shop.Infrastructure.Inventory;
using Shop.Inventory.DataProvider.Repositories;

namespace Shop.Inventory.Api.Consumers;

public class ProductAllocateConsumer: IConsumer<ProductAllocateCommand>
{
    private readonly IStockRepository _stockRepository;

    public ProductAllocateConsumer(IStockRepository stockRepository)
    {
        _stockRepository = stockRepository;
    }
    
    public Task Consume(ConsumeContext<ProductAllocateCommand> context)
    {
        return _stockRepository.AllocateProductsAsync(context.Message);
    }
}