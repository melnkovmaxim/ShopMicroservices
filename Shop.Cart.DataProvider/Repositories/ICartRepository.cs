
namespace Shop.Cart.DataProvider.Repositories;

using Shop.Infrastructure.Cart;

public interface ICartRepository
{
    Task AddCartAsync(Cart cart);
    Task<Cart> GetUserCartOrDefaultAsync(string userId);
    Task RemoveUserCartAsync(string cartId);
}