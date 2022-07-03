using System.Text.Json;
using Microsoft.Extensions.Caching.Distributed;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace Shop.Cart.DataProvider.Repositories;

using Shop.Infrastructure.Cart;

public class CartRepository: ICartRepository
{
    private readonly IDistributedCache _distributedCache;

    public CartRepository(IDistributedCache distributedCache)
    {
        _distributedCache = distributedCache;
    }
    
    public Task AddCartAsync(Cart cart)
    {
        return _distributedCache.SetStringAsync(cart.UserId, JsonSerializer.Serialize(cart));
    }

    public async Task<Cart> GetUserCartOrDefaultAsync(string userId)
    {
        var rawCart = await _distributedCache.GetStringAsync(userId);

        if (rawCart is null)
        {
            return new Cart();
        }

        return JsonSerializer.Deserialize<Cart>(rawCart);
    }

    public Task RemoveUserCartAsync(string cartId)
    {
        return _distributedCache.RemoveAsync(cartId);
    }
}