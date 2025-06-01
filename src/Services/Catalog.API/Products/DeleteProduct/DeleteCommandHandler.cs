using Catalog.API.Models;

namespace Catalog.Api.Products.DeleteProduct;

public record DeleteCommand(string id): ICommand<DeleteCommandResponse>;

public record DeleteCommandResponse(string message);

public class DeleteCommandValidator : AbstractValidator<DeleteCommand>
{
    public DeleteCommandValidator()
    {
        RuleFor(x => x.id).NotEmpty().Must(BeValidGuid).WithMessage("ID must be a valid GUID format");

    }

    private bool BeValidGuid(string id)
    {
        return Guid.TryParse(id, out _);
    }
}


public class DeleteCommandHandler(IDocumentSession session): ICommandHandler<DeleteCommand, DeleteCommandResponse>
{
    public async Task<DeleteCommandResponse> Handle(DeleteCommand request, CancellationToken cancellationToken)
    {
        var idToGuid = Guid.Parse(request.id);
    

        try
        {
            var productFound = await session.LoadAsync<Product>(idToGuid, cancellationToken);

            if (productFound == null)
            {
                return new DeleteCommandResponse("Product not found");
            }
            
            session.Delete(productFound);
        
        
            await session.SaveChangesAsync(cancellationToken);
        }catch(Exception e)
        {
            return new DeleteCommandResponse(e.Message);
        }
        
        
        return new DeleteCommandResponse("Successfully Deleted");
    }
}