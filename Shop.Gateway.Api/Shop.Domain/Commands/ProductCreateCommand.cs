namespace Shop.Domain.Commands;

public class ProductCreateCommand
{
    public Guid ProductId { get; set; }
    public string ProductName { get; set; } = null!;
    public string ProductDescription { get; set; } = null!;
    public decimal ProductPrice { get; set; }
    public Guid CategoryId { get; set; }
}