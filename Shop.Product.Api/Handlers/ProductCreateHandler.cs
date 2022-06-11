using MassTransit;
using Shop.Domain.Commands;
using Shop.Product.Api.Services;

namespace Shop.Product.Api.Handlers;

public class ProductCreateHandler: IConsumer<ProductCreateCommand>
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
}