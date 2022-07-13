namespace Shop.Infrastructure.Inventory;

public class ProductAllocateCommand
{
    public List<StockProduct> Products { get; set; } = null!;
}