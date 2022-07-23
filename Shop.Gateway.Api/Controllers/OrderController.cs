using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Shop.Infrastructure.Order;

namespace Shop.Gateway.Api.Controllers;

[Route("api/v1/[controller]")]
[ApiController]
public class OrderController: ControllerBase
{
    private readonly IRequestClient<Order> _requestClient;

    public OrderController(IRequestClient<Order> requestClient)
    {
        _requestClient = requestClient;
    }

    [HttpPost]
    public async Task<IActionResult> PlaceOrder(Order order)
    {
        try
        {
            var result = await _requestClient.GetResponse<OrderPlacedEvent>(order);
            
            return Accepted();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}