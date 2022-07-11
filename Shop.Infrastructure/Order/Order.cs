using System.Text.Json.Serialization;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Shop.Infrastructure.Order;

public class Order
{
    [BsonId]
    public string OrderId { get; set; } = null!;
    public string UserId { get; set; } = null!;
    public List<OrderItem> Items { get; set; } = null!;
    public decimal Amount => Items?.Sum(x => x.Price * x.Quantity) ?? 0;
}