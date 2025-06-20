namespace Ordering.Domain.Models;

public class Order: Aggregate<OrderId>
{
    public CustomerId CustomerId { get; private set; }
    public OrderName OrderName { get; private set; }
    public IReadOnlyList<OrderItem> OrderItems { get; private set; }
    
    public decimal TotalAmount
    {
        get => OrderItems.Sum(x => x.Price * x.Quantity);
    }

    public Address DeliveryAddress { get;private set; }
    public Address BillingAddress { get; private set; }
    public Payment Payment { get; private set; } = default;
    public OrderStatus Status { get;private  set; } = OrderStatus.Pending;

    public static Order Create(CustomerId customerId, OrderName orderName, IReadOnlyList<OrderItem> orderItems,
        Address deliveryAddress, Address billingAddress)
    {
        ArgumentNullException.ThrowIfNull(customerId);
        ArgumentNullException.ThrowIfNull(orderName);
        ArgumentNullException.ThrowIfNull(deliveryAddress);
        ArgumentNullException.ThrowIfNull(billingAddress);
        ArgumentNullException.ThrowIfNull(orderItems);
        ArgumentOutOfRangeException.ThrowIfGreaterThan(orderItems.Count, 0);
        
        return new Order
        {
            CustomerId = customerId,
            OrderName = orderName,
            OrderItems = orderItems,
            DeliveryAddress = deliveryAddress,
            BillingAddress = billingAddress
        };
    }
}