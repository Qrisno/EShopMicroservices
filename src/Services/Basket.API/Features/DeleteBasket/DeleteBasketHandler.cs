using Basket.API.Features.GetBasket;
using Basket.API.Models;
using BuildingBlocks.CQRS;
using Marten;

namespace Basket.API.Features.DeleteBasket;
public record DeleteBasketResult(bool Success);

public record DeleteBasketCommand(string UserName): ICommand<DeleteBasketResult>;
public class DeleteBasketCommandHandler(IDocumentSession session): ICommandHandler<DeleteBasketCommand, DeleteBasketResult>
{
    public async Task<DeleteBasketResult> Handle(DeleteBasketCommand request, CancellationToken cancellationToken)
    {
        var cart = session.Query<ShoppingCart>().FirstOrDefault(cart => cart.UserName == request.UserName);
        if (cart != null)
        {
            session.Delete(cart);
            return new DeleteBasketResult(true);
        }
        
        return new DeleteBasketResult(false);
    }
}