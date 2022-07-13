namespace Shop.Infrastructure.Inventory;

public class ProductReleaseCommand
{
    public List<StockProduct> Products { get; set; } = null!;
}