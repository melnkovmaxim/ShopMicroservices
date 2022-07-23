namespace Shop.Infrastructure.Order;

public class OrderPlacedEvent
{
    public string OrderId { get; set; } = null!;
    public string RequestId { get; set; } = null!;
}