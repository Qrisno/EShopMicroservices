using Catalog.API.Models;

namespace Catalog.API.Products.CreateProduct;

public record CreateProductCommand(ProductDto Product) : ICommand<CreateProductResult>;
public record CreateProductResult(Guid id);
public class CreateProductCommandHandler(IDocumentSession session): ICommandHandler<CreateProductCommand, CreateProductResult>
{
    public async Task<CreateProductResult> Handle(CreateProductCommand command, CancellationToken cancellationToken)
    {
        var newProduct = new Product
        {
            Name = command.Product.Name,
            Description = command.Product.Description, 
            Category = command.Product.Category,
            ImageFile = command.Product.ImageFile, 
            Price = command.Product.Price,
        };
        
        session.Store(newProduct);
        await session.SaveChangesAsync(cancellationToken);
        //save to db
        return new CreateProductResult(newProduct.Id);
    }
}