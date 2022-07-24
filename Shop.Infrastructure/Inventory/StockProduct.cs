using Shop.Infrastructure.Repositories;

namespace Shop.Infrastructure.Inventory;

public class StockProduct: IEntity
{
    public string Id { get; set; } = null!;
    public int Quantity { get; set; }
}