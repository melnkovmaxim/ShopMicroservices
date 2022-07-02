
namespace Shop.Cart.DataProvider.Repositories;

using Shop.Infrastructure.Cart;

public interface ICartRepository
{
    Task<bool> AddCartAsync(Cart cart);
    Task<Cart> GetUserCartAsync(string userId);
    Task<bool> RemoveUserCartAsync(string cartId);
}