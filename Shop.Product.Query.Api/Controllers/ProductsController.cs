using Microsoft.AspNetCore.Mvc;
using Shop.Domain.Events;
using Shop.Product.DataProvider.Services;

namespace Shop.Product.Query.Api.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class ProductsController
{
    private readonly IProductService _productService;

    public ProductsController(IProductService productService)
    {
        _productService = productService;
    }
    
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<ProductCreatedEvent>> GetProduct(string id) => await _productService.GetProductAsync(id);
}