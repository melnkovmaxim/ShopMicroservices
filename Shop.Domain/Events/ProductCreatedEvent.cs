namespace Shop.Domain.Events;

public class ProductCreatedEvent
{
    public string ProductId { get; set; }
    public string ProductName { get; set; } = null!;
    public DateTime CreatedAt { get; set; }
}