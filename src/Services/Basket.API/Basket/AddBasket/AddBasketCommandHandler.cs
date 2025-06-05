using Basket.API.Models;

namespace Basket.API.Basket.AddBasket;

public record AddBasketCommand(ShoppingCartItem item);
public record AddBasketResponse(string Error);

public class AddBasketCommandHandler
{
    
}