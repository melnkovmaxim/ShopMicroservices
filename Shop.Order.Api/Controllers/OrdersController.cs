using Microsoft.AspNetCore.Mvc;
using Shop.Order.DataProvider.Repositories;

namespace Shop.Order.Api.Controllers;

using Infrastructure.Order;

[Route("api/v1/[controller]")]
[ApiController]
public class OrdersController: ControllerBase
{
    private readonly IOrderRepository _orderRepository;

    public OrdersController(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }
    
    [HttpGet("{orderId}")]
    public async Task<IActionResult> GetOrder(string orderId)
    {
        var order = await _orderRepository.GetOrderAsync(orderId);

        return Ok(order);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllOrders([FromQuery] string userId)
    {
        var orders = _orderRepository.GetAllUserOrdersAsync(userId);

        return Ok(orders);
    }

    [HttpPost]
    public async Task<IActionResult> CreateOrder(Order order)
    {
        await _orderRepository.CreateOrderAsync(order);
        
        return NoContent();
    }
}