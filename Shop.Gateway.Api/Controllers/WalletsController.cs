using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Shop.Infrastructure.Wallet;

namespace Shop.Gateway.Api.Controllers;

[Route("api/v1/[controller]")]
[ApiController]
public class WalletsController: ControllerBase
{
    private readonly IBus _bus;

    public WalletsController(IBus bus)
    {
        _bus = bus;
    }
    
    [HttpPut("add")]
    public async Task<IActionResult> AddFunds(FundsAddCommand command)
    {
        var endpoint = await _bus.GetSendEndpoint(new Uri("rabbitmq://localhost:5672/add_funds"));
        await endpoint.Send(command);

        return Accepted();
    }
    
    [HttpPut("deduct")]
    public async Task<IActionResult> DeductFunds(FundsDeductCommand command)
    {
        var endpoint = await _bus.GetSendEndpoint(new Uri("rabbitmq://localhost:5672/deduct_funds"));
        await endpoint.Send(command);

        return Accepted();
    }
}