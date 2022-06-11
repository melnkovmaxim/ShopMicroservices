using System.Net;
using Microsoft.AspNetCore.Mvc;
using Shop.Domain.Commands;
using Shop.Domain.Events;
using Shop.Product.Api.Services;

namespace Shop.Product.Api.Controllers;

[Route("api/v1/[controller]")]
[ApiController]
public class ProductsController: ControllerBase
{
    private readonly IProductService _productService;

    public ProductsController(IProductService productService)
    {
        _productService = productService;
    }
    
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<ProductCreatedEvent>> GetProduct(string id) => await _productService.GetProductAsync(id);

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<ProductCreatedEvent>> AddProduct([FromBody] ProductCreateCommand command) => await _productService.AddProductAsync(command);
}