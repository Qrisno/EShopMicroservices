using Catalog.API.Models;

namespace Catalog.Api.Products.GetProductByCategory;

public record GetProductByCategoryQuery(string category) : IQuery<GetProductByCategoryResult>;
public record GetProductByCategoryResult(IEnumerable<Product> Products);
public class GetProductByCategoryQueryHandler(IDocumentSession session): IQueryHandler<GetProductByCategoryQuery, GetProductByCategoryResult>
{
    public async Task<GetProductByCategoryResult> Handle(GetProductByCategoryQuery request, CancellationToken cancellationToken)
    {
        var products = await session.Query<Product>().ToListAsync(cancellationToken);
        if (products.Count == 0)
        {
            return new GetProductByCategoryResult(null);
        }
        
        var filteredProducts = products.Where(x => x.Category.Contains(request.category));

        return new GetProductByCategoryResult(filteredProducts);
    }
}