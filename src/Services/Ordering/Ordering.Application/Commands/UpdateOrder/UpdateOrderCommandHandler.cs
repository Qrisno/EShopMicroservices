using BuildingBlocks.CQRS;
using Ordering.Application.Abstractions;
using Ordering.Domain.Models;
using Ordering.Domain.ValueObjects;

namespace Ordering.Application.Commands.UpdateOrder;

public class UpdateOrderCommandHandler(IApplicationDbContext context): ICommandHandler<UpdateOrderCommand, Order>
{
    public async Task<Order> Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
    {
        var orderToUpdate = Order.Create(request.CustomerId, request.OrderName, request.OrderItems,
            request.DeliveryAddress, request.BillingAddress);
        orderToUpdate.Id = OrderId.Of(request.OrderId);
        context.Orders.Update(orderToUpdate);
        await context.SaveChangesAsync(cancellationToken);
        
        return orderToUpdate;
    }
}