using System.Net;
using Microsoft.AspNetCore.Mvc;
using Shop.Domain.Commands;
using Shop.Domain.Events;
using Shop.Product.DataProvider.Services;

namespace Shop.Product.Api.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class ProductsController: ControllerBase
{
    private readonly IProductService _productService;

    public ProductsController(IProductService productService)
    {
        _productService = productService;
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<ProductCreatedEvent>> AddProduct([FromBody] ProductCreateCommand command) => await _productService.AddProductAsync(command);
}