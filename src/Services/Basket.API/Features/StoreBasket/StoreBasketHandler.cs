using Basket.API.Data;
using Basket.API.Models;
using BuildingBlocks.CQRS;
using FluentValidation;
using Marten;

namespace Basket.API.Features.StoreBasket;

public record StoreBasketResult(ShoppingCart cart);

public record StoreBasketCommand(ShoppingCart cart) : ICommand<StoreBasketResult>;
public class StoreBasketCommandValidator : AbstractValidator<StoreBasketCommand>
{
    public StoreBasketCommandValidator()
    {
        RuleFor(c=>c.cart).NotNull();
        RuleFor(c=> c.cart.Items).NotNull();
        RuleFor(c=> c.cart.UserName).NotEmpty()
            .Matches("^(?!.*[._]{2})[a-zA-Z][a-zA-Z0-9._]{1,18}[a-zA-Z0-9]$")
            .WithMessage("Username must be 3–20 characters, start with a letter, and can include letters, numbers, underscores, or dots, but not consecutively or at the end.");
    }
}
public class StoreBasketHandler(IBasketRepository repo): ICommandHandler<StoreBasketCommand, StoreBasketResult>
{
    public async Task<StoreBasketResult> Handle(StoreBasketCommand request, CancellationToken cancellationToken)
    {
        var cart = new ShoppingCart(request.cart.UserName);
        cart.Items.AddRange(request.cart.Items);
        await repo.StoreBasket(cart, cancellationToken);
        return new StoreBasketResult(cart);
    }
}