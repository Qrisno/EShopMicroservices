using Carter;

using Catalog.API.Models;

namespace Catalog.API.Products.CreateProduct;


public record CreateProductRequest(ProductDto Product);
public record CreateProductResponse(Guid id);

public class CreateProductEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder endpointRouteBuilder)
    {

    }
}