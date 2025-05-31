namespace Catalog.Api.Products.GetProducts;

public class GetProductsEndpoint: ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/GetProducts", async (ISender sender) =>
        {
            var res = await sender.Send(new GetProductsQuery());

            return res.Products;
        });
    }
}