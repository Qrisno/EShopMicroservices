namespace Basket.API.Models;

public class ShoppingCartItem
{
    public int Quantity { get; set; }
    public Guid ProductId { get; set; }
    public string Color { get; set; } = string.Empty;
    public string ProductBane { get; set; } = string.Empty;
    public decimal Price { get; set; }
}