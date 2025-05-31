

using Catalog.API.Models;
using FastExpressionCompiler;

namespace Catalog.Api.Products.GetProduct;

public record GetProductQuery(Guid id) :  IQuery<GetProductResult>;
public record GetProductResult(Product? Product);

public class GetProductQueryHandler(IDocumentSession session): IQueryHandler<GetProductQuery,GetProductResult>
{
    public async Task<GetProductResult> Handle(GetProductQuery request, CancellationToken cancellationToken)
    {
        var res =  await session.LoadAsync<Product>(request.id, cancellationToken);
        if (res != null)
        {
            return new GetProductResult(res);
        }

        return new GetProductResult(null);
    }
}