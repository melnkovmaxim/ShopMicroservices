using MassTransit;
using Shop.Domain.Commands;
using Shop.Domain.Queries;
using Shop.Product.DataProvider.Services;

namespace Shop.Product.Query.Api.Handlers;

public class GetProductByIdHandler: IConsumer<GetProductByIdQuery>
{
    private readonly IProductService _productService;

    public GetProductByIdHandler(IProductService productService)
    {
        _productService = productService;
    }

    public async Task Consume(ConsumeContext<GetProductByIdQuery> context)
    {
        var product = await _productService.GetProductAsync(context.Message.ProductId);

        await context.RespondAsync(product);
    }
}