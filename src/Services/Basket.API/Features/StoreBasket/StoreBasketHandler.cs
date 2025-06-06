using Basket.API.Models;
using BuildingBlocks.CQRS;
using Marten;

namespace Basket.API.Features.StoreBasket;

public record StoreBasketResult(ShoppingCart cart);

public record StoreBasketCommand(ShoppingCart cart) : ICommand<StoreBasketResult>;

public class StoreBasketHandler(IDocumentSession session): ICommandHandler<StoreBasketCommand, StoreBasketResult>
{
    public Task<StoreBasketResult> Handle(StoreBasketCommand request, CancellationToken cancellationToken)
    {
        var cart = new ShoppingCart(request.cart.UserName);
        cart.Items.AddRange(request.cart.Items);
        session.Store(cart);
        
        return Task.FromResult(new StoreBasketResult(cart));
    }
}