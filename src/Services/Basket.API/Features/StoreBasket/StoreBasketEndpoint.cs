using Basket.API.Models;
using Carter;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Basket.API.Features.StoreBasket;

public class StoreBasketEndpoint: ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/bakset", async ([FromBody] ShoppingCart cart, ISender sender) =>
        {
            await sender.Send(new StoreBasketCommand(cart));
            return Results.Ok(cart);
        });
    }
}