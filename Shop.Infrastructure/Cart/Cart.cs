namespace Shop.Infrastructure.Cart;

public class Cart
{
    public string CartId { get; set; } = null!;
    public decimal Amount { get; set; }
    public string UserId { get; set; } = null!;
}