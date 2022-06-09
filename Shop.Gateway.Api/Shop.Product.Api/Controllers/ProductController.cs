using System.Net;
using Microsoft.AspNetCore.Mvc;
using Shop.Domain.Commands;

namespace Shop.Product.Api.Controllers;

[Route("api/v1/[controller]")]
[ApiController]
public class ProductController: ControllerBase
{
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status202Accepted)]
    public IActionResult GetProduct() => Accepted();

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status202Accepted)]
    public IActionResult AddProduct([FromBody] ProductCreateCommand command) => Accepted();
}