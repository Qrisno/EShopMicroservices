using Carter;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Basket.API.Features.DeleteBasket;

public class DeleteBasketEndpoint: ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapDelete("/basker/{username}", async ([FromRoute] string username, ISender sender) =>
        {
            var res = await sender.Send(new DeleteBasketCommand(username));
            
            if(res.Success) return Results.Ok();
            return Results.NotFound();
        });
    }
    
}