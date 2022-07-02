namespace Shop.Infrastructure.Cart;

public class CartItem
{
    public string ProductId { get; set; } = null!;
    public int Quantity { get; set; }
    public decimal Amount { get; set; }
    public string UserId { get; set; } = null!;
}