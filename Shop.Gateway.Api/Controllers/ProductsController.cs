using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Shop.Domain.Commands;

namespace Shop.Gateway.Api.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class ProductsController: ControllerBase
{
    private readonly IBusControl _bus;

    public ProductsController(IBusControl bus)
    {
        _bus = bus;
    }
    
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status202Accepted)]
    public IActionResult GetProduct() => Accepted();

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