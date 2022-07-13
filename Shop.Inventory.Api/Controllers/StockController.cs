using Microsoft.AspNetCore.Mvc;
using Shop.Infrastructure.Inventory;
using Shop.Inventory.DataProvider.Repositories;

namespace Shop.Inventory.Api.Controllers;

[Route("api/v1/[controller]")]
[ApiController]
public class StockController: ControllerBase
{
    private readonly IStockRepository _stockRepository;

    public StockController(IStockRepository stockRepository)
    {
        _stockRepository = stockRepository;
    }

    [HttpPut("allocate")]
    public async Task<IActionResult> AllocateProduct(ProductAllocateCommand command)
    {
        await _stockRepository.AllocateProductsAsync(command);

        return NoContent();
    }

    [HttpPut("release")]
    public async Task<IActionResult> ReleaseProduct(ProductReleaseCommand command)
    {
        await _stockRepository.ReleaseProductsAsync(command);

        return NoContent();
    }
}