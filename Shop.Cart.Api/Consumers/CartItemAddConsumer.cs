using MassTransit;
using Shop.Cart.DataProvider.Repositories;

namespace Shop.Cart.Api.Consumers;

using Shop.Infrastructure.Cart;

public class CartItemAddConsumer: IConsumer<Cart>
{
    private readonly ICartRepository _cartRepository;

    public CartItemAddConsumer(ICartRepository cartRepository)
    {
        _cartRepository = cartRepository;
    }
    
    public Task Consume(ConsumeContext<Cart> context)
    {
        return _cartRepository.AddCartAsync(context.Message);
    }
}