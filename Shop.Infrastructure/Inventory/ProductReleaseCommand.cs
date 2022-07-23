namespace Shop.Infrastructure.Inventory;

public class ProductReleaseCommand
{
    public List<StockProduct> Items { get; set; } = null!;
}