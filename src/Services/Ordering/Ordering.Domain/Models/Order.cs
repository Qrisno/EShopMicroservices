

namespace Ordering.Domain.Models;

public class Order: Aggregate<Guid>
{
    public Guid CustomerId { get; set; }
    public string OrderName { get; set; }
    public IReadOnlyList<OrderItem> OrderItems { get; }
    
    public decimal TotalAmount
    {
        get => OrderItems.Sum(x => x.Price * x.Quantity);
    }

    public Address DeliveryAddress { get; set; }
    public Address BillingAddress { get; set; }
    public Payment Payment { get; private set; } = default;
    public OrderStatus Status { get; set; } = OrderStatus.Pending;
}