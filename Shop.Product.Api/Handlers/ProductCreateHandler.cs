using MassTransit;
using Shop.Domain.Commands;
using Shop.Domain.Events;
using Shop.Product.DataProvider.Services;

namespace Shop.Product.Api.Handlers;

public class ProductCreateHandler: IConsumer<ProductCreateCommand>, IConsumer<ProductCreatedEvent>
{
    private readonly IProductService _productService;

    public ProductCreateHandler(IProductService productService)
    {
        _productService = productService;
    }
    
    public Task Consume(ConsumeContext<ProductCreateCommand> context)
    {
        return _productService.AddProductAsync(context.Message);
    }

    public Task Consume(ConsumeContext<ProductCreatedEvent> context)
    {
        throw new NotImplementedException();
    }
}