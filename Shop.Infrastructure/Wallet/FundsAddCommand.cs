namespace Shop.Infrastructure.Wallet;

public class FundsAddCommand
{
    public string UserId { get; set; } = null!;
    public decimal CreditAmount { get; set; }
}