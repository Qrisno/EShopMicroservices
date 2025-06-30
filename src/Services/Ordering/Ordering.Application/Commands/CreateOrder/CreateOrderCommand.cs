using BuildingBlocks.CQRS;
using Ordering.Domain.Models;
using Ordering.Domain.ValueObjects;

public record CreateOrderCommand
(
    CustomerId CustomerId,
    OrderName OrderName,
    IReadOnlyList<OrderItem> OrderItems,
    Address DeliveryAddress,
    Address BillingAddress
): ICommand<Guid>;