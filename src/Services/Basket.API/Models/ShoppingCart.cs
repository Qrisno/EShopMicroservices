using Marten.Schema;

namespace Basket.API.Models;

public class ShoppingCart
{
    public string UserName { get; set; } = string.Empty;
    public List<ShoppingCartItem> Items { get; set; } = new();
    public decimal Total => Items.Sum(i => i.Price * i.Quantity);

    public ShoppingCart(string userName)
    {
        UserName = userName;
    }

    public ShoppingCart()
    {
        
    }
}