using Microsoft.AspNetCore.Mvc;
using Shop.Cart.DataProvider.Repositories;

namespace Shop.Cart.Api.Controllers;

using Shop.Infrastructure.Cart;

[ApiController]
[Route("[controller]")]
public class CartController : ControllerBase
{
    private readonly ICartRepository _cartRepository;

    public CartController(ICartRepository cartRepository)
    {
        _cartRepository = cartRepository;
    }

    [HttpGet]
    public async Task<IActionResult> Get(string userId)
    {
        var cart = await _cartRepository.GetUserCartAsync(userId);

        return Ok(cart);
    }

    [HttpPost]
    public async Task<IActionResult> Add(Cart cart)
    {
        await _cartRepository.AddCartAsync(cart);

        return NoContent();
    }
}