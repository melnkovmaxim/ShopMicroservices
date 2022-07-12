using MongoDB.Bson.Serialization.Attributes;

namespace Shop.Infrastructure.Wallet;

public class Wallet
{
    [BsonId]
    public string UserId { get; set; } = null!;
    public decimal Amount { get; set; }
}