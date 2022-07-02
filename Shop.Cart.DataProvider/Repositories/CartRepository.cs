using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace Shop.Cart.DataProvider.Repositories;

using Shop.Infrastructure.Cart;

public class CartRepository: ICartRepository
{
    private readonly IMongoDatabase _mongo;
    private readonly IMongoCollection<Cart> _carts;

    public CartRepository(IMongoDatabase mongo)
    {
        _mongo = mongo;
        _carts = mongo.GetCollection<Cart>("cart");
    }
    
    public Task<bool> AddCartAsync(Cart cart)
    {
        _carts.InsertOne(cart);
    }

    public Task<Cart> GetUserCartAsync(string userId)
    {
        _carts.AsQueryable()
            .FirstOrDefaultAsync(x => x.)
    }

    public Task<bool> RemoveUserCartAsync(string cartId)
    {
        throw new NotImplementedException();
    }
}