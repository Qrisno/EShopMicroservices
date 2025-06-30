using BuildingBlocks.CQRS;
using Microsoft.EntityFrameworkCore;
using Ordering.Application.Abstractions;
using Ordering.Domain.Models;
using Ordering.Domain.ValueObjects;

namespace Ordering.Application.Queries.GetOrder;

public class GetOrderQueryHandler(IApplicationDbContext context): IQueryHandler<GetOrderQuery, Order>
{
    public async Task<Order?> Handle(GetOrderQuery request, CancellationToken cancellationToken)
    {
       var orderFound =  await context.Orders.FirstOrDefaultAsync(o=> o.Id.Value == request.OrderId);
       
       if (orderFound == null)
       {
           return null;
       }

       return orderFound;
    }
}