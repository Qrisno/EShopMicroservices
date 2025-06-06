using Basket.API.Models;
using BuildingBlocks.CQRS;
using Marten;

namespace Basket.API.Features.GetBasket;

public record GetBasketQuery(string UserName) : IQuery<GetBasketResult>;
public record GetBasketResult(ShoppingCart Cart);

public class GetBasketQueryHandler(IDocumentSession session): IQueryHandler<GetBasketQuery, GetBasketResult>
{
    public async Task<GetBasketResult> Handle(GetBasketQuery request, CancellationToken cancellationToken)
    {
        var basket =  session.Query<ShoppingCart>().FirstOrDefault(cart=> cart.UserName == request.UserName);

        if (basket == null)
        {
            return new GetBasketResult(null);
        }
        
        return new GetBasketResult(basket);
    }
}