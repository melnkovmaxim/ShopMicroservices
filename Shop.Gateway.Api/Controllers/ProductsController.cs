using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Shop.Domain.Commands;
using Shop.Domain.Events;
using Shop.Domain.Queries;

namespace Shop.Gateway.Api.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class ProductsController: ControllerBase
{
    private readonly IBusControl _bus;
    private readonly IRequestClient<GetProductByIdQuery> _getByIdRequest;

    public ProductsController(IBusControl bus, IRequestClient<GetProductByIdQuery> getByIdRequest)
    {
        _bus = bus;
        _getByIdRequest = getByIdRequest;
    }

    [HttpGet]
    [ProducesResponseType(typeof(ProductCreatedEvent), StatusCodes.Status202Accepted)]
    public async Task<IActionResult> GetProduct(string productId)
    {
        var command = new GetProductByIdQuery()
        {
            ProductId = productId
        };
        
        var product = await _getByIdRequest.GetResponse<ProductCreatedEvent>(command);

        return Ok(product);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status202Accepted)]
    public async Task<IActionResult> AddProduct([FromBody] ProductCreateCommand command)
    {
        var uri = new Uri("rabbitmq://localhost:5672/create_product");
        var endpoint = await _bus.GetSendEndpoint(uri);

        await endpoint.Send(command);

        return Accepted();
    }
}