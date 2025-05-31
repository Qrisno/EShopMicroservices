using Microsoft.AspNetCore.Mvc;

namespace Catalog.Api.Products.GetProductByCategory;

public class GetProductByCategoryEndpoint: ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/GetProductByCategory/{category}", async ([FromRoute] string category, ISender sender) =>
        {
            var res = await sender.Send(new GetProductByCategoryQuery(category));
            return Results.Ok(res);
        });
    }
}