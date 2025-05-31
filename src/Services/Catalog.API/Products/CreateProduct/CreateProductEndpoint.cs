using Catalog.API.Models;

namespace Catalog.API.Products.CreateProduct;


public record CreateProductRequest(ProductDto Product);
public record CreateProductResponse(Guid id);

public class CreateProductEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder endpointRouteBuilder)
    {
        endpointRouteBuilder.MapPost("/products", async (CreateProductRequest req, ISender sender) =>
        {
            var command = req.Adapt<CreateProductCommand>();
            var result = await sender.Send(command);
            var response = new CreateProductResponse(result.id);

            return Results.Created("Created with product", response);

        });
    }
}