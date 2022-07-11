using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Shop.Infrastructure.Cart;

public class Cart
{
    public string CartId { get; set; } = null!;
    public decimal Amount { get; set; }
    public string UserId { get; set; } = null!;
    public List<CartItem>? Items { get; set; }
}