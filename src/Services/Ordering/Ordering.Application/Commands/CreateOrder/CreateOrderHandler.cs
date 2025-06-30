using BuildingBlocks.CQRS;
using Ordering.Application.Abstractions;
using Ordering.Domain.Models;

namespace Ordering.Application.Commands;

public class CreateOrderHandler(IApplicationDbContext context): ICommandHandler<CreateOrderCommand, Guid>
{
    public async Task<Guid> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        var newOrder = Order.Create(request.CustomerId, request.OrderName, request.OrderItems,
            request.DeliveryAddress, request.BillingAddress);
        await context.Orders.AddAsync(newOrder, cancellationToken);

        await context.SaveChangesAsync(cancellationToken);
        
        return newOrder.Id.Value;
    }
}