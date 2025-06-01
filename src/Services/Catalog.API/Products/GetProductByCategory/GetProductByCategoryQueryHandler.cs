using BuildingBlocks.Exceptions;
using Catalog.Api.Exceptions;
using Catalog.API.Models;

namespace Catalog.Api.Products.GetProductByCategory;

public record GetProductByCategoryQuery(string category) : IQuery<GetProductByCategoryResult>;
public record GetProductByCategoryResult(IEnumerable<Product> Products);
public class GetProductByCategoryQueryHandler(IDocumentSession session): IQueryHandler<GetProductByCategoryQuery, GetProductByCategoryResult>
{
    public async Task<GetProductByCategoryResult> Handle(GetProductByCategoryQuery request, CancellationToken cancellationToken)
    {
        var products = await session.Query<Product>().ToListAsync(cancellationToken);
        
        var filteredProducts = products.Where(x => x.Category.Contains(request.category)).ToList();

        if (filteredProducts.Count == 0)
        {
            throw new NotFoundException("Product with category",request.category);
        }
        return new GetProductByCategoryResult(filteredProducts);
    }
}