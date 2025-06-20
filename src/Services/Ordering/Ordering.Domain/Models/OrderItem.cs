

namespace Ordering.Domain.Models;

public class OrderItem: Entity<OrderItemId>
{
    public OrderId OrderId { get; set; }
    public ProductId ProductId { get; set; }
    public decimal Price { get; set; }
    public int Quantity { get; set; }
}