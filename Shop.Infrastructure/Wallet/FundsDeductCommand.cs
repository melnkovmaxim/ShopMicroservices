namespace Shop.Infrastructure.Wallet;

public class FundsDeductCommand
{
    public string UserId { get; set; } = null!;
    public decimal DebitAmount { get; set; }
}