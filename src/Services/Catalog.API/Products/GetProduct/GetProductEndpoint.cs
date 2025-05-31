using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.Api.Products.GetProduct;

public class GetProductEndpoint: ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/GetProduct/{id}", async ([FromRoute] Guid id, ISender sender) =>
        {
            var query = new GetProductQuery(id);
            var result = await sender.Send(query);

            return result.Product == null ? Results.NotFound() : Results.Ok(result);
        });
    }
}