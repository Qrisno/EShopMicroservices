

namespace Ordering.Domain.Models;

public class OrderItem: Entity<OrderItemId>
{
    public OrderId OrderId { get; private set; }
    public ProductId ProductId { get; private set; }
    public decimal Price { get; private set; }
    public int Quantity { get; private set; }

    public static OrderItem Create(OrderId orderId, ProductId productId, decimal price, int quantity)
    {
        ArgumentNullException.ThrowIfNull(orderId);
        ArgumentNullException.ThrowIfNull(productId);
        ArgumentOutOfRangeException.ThrowIfLessThan(quantity, 1);
        if (price <= 0)
            throw new DomainException("Price cannot be less than or equal to zero");
        
        return new OrderItem
        {
            Id = OrderItemId.Of(Guid.NewGuid()),
            OrderId = orderId,
            ProductId = productId,
            Price = price,
            Quantity = quantity
        };
    }
    
}