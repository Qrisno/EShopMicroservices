using Catalog.API.Models;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.Api.Products.UpdateProduct;

public class UpdateProductEndpoint: ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPut("/UpdateProduct", async ([FromBody] UpdateProductDto product, ISender sender) =>
        {
            try
            {
                var res = await sender.Send(new UpdateProductCommand(product));
                return Results.Ok(res); 
            }
            catch (Exception e)
            {
                return Results.NotFound(e.Message);
            }
            
            
            
        });
    }
}