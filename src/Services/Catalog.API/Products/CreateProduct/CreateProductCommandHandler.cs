using Catalog.API.Models;
using FastExpressionCompiler;
using FluentValidation;
using ValidationException = System.ComponentModel.DataAnnotations.ValidationException;

namespace Catalog.API.Products.CreateProduct;

public record CreateProductCommand(ProductDto Product) : ICommand<CreateProductResult>;
public record CreateProductResult(Guid id);


public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
{
    public CreateProductCommandValidator()
    {
        RuleFor(x => x.Product).NotNull();
        RuleFor(x => x.Product.Name).MaximumLength(1000).NotEmpty().WithMessage("Name is required");
        RuleFor(x => x.Product.Description).NotEmpty();
        RuleFor(x => x.Product.Category).NotEmpty();
        RuleFor(x => x.Product.ImageFile).NotEmpty().Matches(@"^.*\.(jpg|jpeg|png|gif|bmp)$");
        RuleFor(x => x.Product.Price).GreaterThan(0).NotEmpty().WithMessage("Price is required");   
    }
}
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