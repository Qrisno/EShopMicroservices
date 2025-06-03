using Microsoft.AspNetCore.Mvc;

namespace Catalog.Api.Products.GetProducts;

public class GetProductsEndpoint: ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/GetProducts/{pageNumber}/{pageSize}", async ([FromRoute]int pageNumber, [FromRoute]int pageSize, ISender sender) =>
        {
            var res = await sender.Send(new GetProductsQuery(pageNumber,pageSize));

            return res.Products;
        });
    }
}