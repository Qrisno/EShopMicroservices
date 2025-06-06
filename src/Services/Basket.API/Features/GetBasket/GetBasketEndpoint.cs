using Carter;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Basket.API.Features.GetBasket;

public class GetBasketEndpoint: ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/basket/{username}", async ([FromRoute] string username, ISender sender) =>
        {
            var res = await sender.Send(new GetBasketQuery(username));
            return Results.Ok(res);
        });
    }
}