namespace Shop.Infrastructure.Wallet;

public class DeductFundsCommand
{
    public string UserId { get; set; } = null!;
    public decimal DebitAmount { get; set; }
}