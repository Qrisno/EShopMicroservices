using Basket.API.Models;
using Carter;
using FluentValidation;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Basket.API.Features.StoreBasket;

public record StoreBasketRequest(ShoppingCart Cart);
public record StoreBasketResponse(string UserName);
public class StoreBasketEndpoint: ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/bakset", async ([FromBody] StoreBasketRequest req, ISender sender) =>
            {
                var command = req.Adapt<StoreBasketCommand>();
                var storeBasket  = await sender.Send(command);

                var res = storeBasket.Adapt<StoreBasketResponse>();
                return Results.Ok(res);
            }).Produces<ShoppingCart>()
            .ProducesProblem(StatusCodes.Status404NotFound)
            .WithSummary("Store Basket");
    }
}