using Basket.API.Data;
using Basket.API.Features.GetBasket;
using Basket.API.Models;
using BuildingBlocks.CQRS;
using Marten;

namespace Basket.API.Features.DeleteBasket;
public record DeleteBasketResult(bool Success);

public record DeleteBasketCommand(string UserName): ICommand<DeleteBasketResult>;
public class DeleteBasketCommandHandler(IBasketRepository repo): ICommandHandler<DeleteBasketCommand, DeleteBasketResult>
{
    public async Task<DeleteBasketResult> Handle(DeleteBasketCommand request, CancellationToken cancellationToken)
    {
        var cart = repo.GetBasket(request.UserName, cancellationToken).Result;
        if (cart != null)
        {
            await repo.DeleteBasket(request.UserName, cancellationToken);
            return new DeleteBasketResult(true);
        }
        
        return new DeleteBasketResult(false);
    }
}