using Catalog.API.Models;

namespace Catalog.Api.Products.UpdateProduct;

public record UpdateProductResult(UpdateProductDto product);
public record UpdateProductCommand(UpdateProductDto Product) : ICommand<UpdateProductResult>;

public class UpdateProductCommandHandler(IDocumentSession session): ICommandHandler<UpdateProductCommand,UpdateProductResult>
{
    public async Task<UpdateProductResult> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        
        var id = Guid.Parse(request.Product.Id);

        var productFound =  session.Query<UpdateProductDto>().Where(x => x.Id.Equals(id));

        if (productFound == null)
        {
            throw new Exception("Product not found");
        }

        var productForUpdate = new Product
        {
            Id = id,
            Name = request.Product.Name,
            Description = request.Product.Description,
            Category = request.Product.Category,
            ImageFile = request.Product.ImageFile,
            Price = request.Product.Price,
        };
        session.Update(productForUpdate);
         await session.SaveChangesAsync(cancellationToken);
         return new UpdateProductResult(request.Product);
    }
}