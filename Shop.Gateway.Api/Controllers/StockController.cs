using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Shop.Infrastructure.Inventory;

namespace Shop.Gateway.Api.Controllers;

[Route("api/v1/[controller]")]
[ApiController]
public class StockController: ControllerBase
{
    private readonly IBus _bus;

    public StockController(IBus bus)
    {
        _bus = bus;
    }

    [HttpPut("allocate")]
    public async Task<IActionResult> AllocateProduct(ProductAllocateCommand command)
    {
        var endpoint = await _bus.GetSendEndpoint(new Uri("rabbitmq://localhost/allocate_product"));

        await endpoint.Send(command);

        return Accepted();
    }

    [HttpPut("release")]
    public async Task<IActionResult> ReleaseProduct(ProductReleaseCommand command)
    {
        var endpoint = await _bus.GetSendEndpoint(new Uri("rabbitmq://localhost/release_product"));

        await endpoint.Send(command);

        return Accepted();
    }
}