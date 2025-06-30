using BuildingBlocks.CQRS;
using Ordering.Domain.Models;
using Ordering.Domain.ValueObjects;

namespace Ordering.Application.Queries.GetOrder;

public record GetOrderQuery
(
    Guid OrderId
    ): IQuery<Order>;