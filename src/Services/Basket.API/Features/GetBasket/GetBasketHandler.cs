using Basket.API.Data;
using Basket.API.Models;
using BuildingBlocks.CQRS;
using Marten;

namespace Basket.API.Features.GetBasket;

public record GetBasketQuery(string UserName) : IQuery<GetBasketResult>;
public record GetBasketResult(ShoppingCart Cart);

public class GetBasketQueryHandler(IBasketRepository repo): IQueryHandler<GetBasketQuery, GetBasketResult>
{
    public async Task<GetBasketResult> Handle(GetBasketQuery request, CancellationToken cancellationToken)
    {
        var basket =   repo.GetBasket(request.UserName, cancellationToken).Result;

        if (basket == null)
        {
            return new GetBasketResult(null);
        }
        
        return new GetBasketResult(basket);
    }
}