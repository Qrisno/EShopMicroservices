using Microsoft.AspNetCore.Mvc;

namespace Catalog.Api.Products.DeleteProduct;

public class DeleteEndpoint: ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapDelete("/DeleteProduct/{id}", async ([FromRoute] string id, ISender sender) =>
        {
            var res = await sender.Send(new DeleteCommand(id));
            
            return Results.Ok(res.message);
        });
    }
}