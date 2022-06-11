using Microsoft.AspNetCore.Mvc;
using Shop.Domain.Commands;

namespace Shop.Gateway.Api.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class ProductsController: ControllerBase
{
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status202Accepted)]
    public IActionResult GetProduct() => Accepted();

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status202Accepted)]
    public IActionResult AddProduct([FromBody] ProductCreateCommand command) => Accepted();
}