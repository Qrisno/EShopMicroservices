using BuildingBlocks.CQRS;
using Ordering.Domain.Models;
using Ordering.Domain.ValueObjects;

namespace Ordering.Application.Commands.UpdateOrder;

public record UpdateOrderCommand
(
    Guid OrderId,
    CustomerId CustomerId,
    OrderName OrderName,
    IReadOnlyList<OrderItem> OrderItems,
    Address DeliveryAddress,
    Address BillingAddress
): ICommand<Order>;