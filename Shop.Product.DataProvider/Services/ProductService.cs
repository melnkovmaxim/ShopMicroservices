using Shop.Domain.Commands;
using Shop.Domain.Events;
using Shop.Product.DataProvider.Repositories;

namespace Shop.Product.DataProvider.Services;

public class ProductService: IProductService
{
    private readonly IProductRepository _productRepository;

    public ProductService(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }
    
    public Task<ProductCreatedEvent> GetProductAsync(string productId) => _productRepository.GetProductAsync(productId);

    public Task<ProductCreatedEvent> AddProductAsync(ProductCreateCommand command)
    {
        return _productRepository.AddProductAsync(command);
    }
}