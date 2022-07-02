using MassTransit;
using Shop.Cart.DataProvider.Repositories;

namespace Shop.Cart.Api.Consumers;

using Shop.Infrastructure.Cart;

public class CartItemRemoveConsumer: IConsumer<Cart>
{
    private readonly ICartRepository _cartRepository;

    public CartItemRemoveConsumer(ICartRepository cartRepository)
    {
        _cartRepository = cartRepository;
    }
    
    public Task Consume(ConsumeContext<Cart> context)
    {
        return _cartRepository.RemoveUserCartAsync(context.Message.UserId);
    }
}