namespace Shop.Infrastructure.Order;

public class OrderItem
{
    public string ProductId { get; set; } = null!;
    public int Quantity { get; set; }
    public decimal Price { get; set; }
}