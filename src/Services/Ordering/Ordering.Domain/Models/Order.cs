

namespace Ordering.Domain.Models;

public class Order: Aggregate<OrderId>
{
    public CustomerId CustomerId { get; private set; }
    public OrderName OrderName { get; private set; }
    public IReadOnlyList<OrderItem> OrderItems => _orderItems.AsReadOnly();
    private List<OrderItem> _orderItems = new();
    
    public decimal TotalAmount => OrderItems.Sum(x => x.Price * x.Quantity);

    public Address DeliveryAddress { get;private set; }
    public Address BillingAddress { get; private set; }
    public Payment Payment { get; private set; } 
    public OrderStatus Status { get;private  set; } = OrderStatus.Pending;

    private Order()
    {
        
    }
    public static Order Create(CustomerId customerId, OrderName orderName, IReadOnlyList<OrderItem> orderItems,
        Address deliveryAddress, Address billingAddress)
    {
        ArgumentNullException.ThrowIfNull(customerId);
        ArgumentNullException.ThrowIfNull(orderName);
        ArgumentNullException.ThrowIfNull(deliveryAddress);
        ArgumentNullException.ThrowIfNull(billingAddress);
        ArgumentNullException.ThrowIfNull(orderItems);
        var order = new Order
        {
            CustomerId = customerId,
            OrderName = orderName,
            DeliveryAddress = deliveryAddress,
            BillingAddress = billingAddress
        };
        order._orderItems.AddRange(orderItems);
        order.AddDomainEvent(new OrderCreatedEvent(order));
        return order;
    }

    public void Update(OrderName name, Address deliveryAddress, Address billingAddress, Payment payment,
        OrderStatus status)
    {
        OrderName = name;
        DeliveryAddress = deliveryAddress;
        BillingAddress = billingAddress;
        Payment = payment;
        Status = status;
        
        AddDomainEvent(
            new OrderUpdatedEvent(this));
    }

    public void Add(OrderId orderId,ProductId prodId, int quantity, decimal price)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(quantity);
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(price);
        var newOrderItem = OrderItem.Create(orderId,prodId, price,quantity);
        _orderItems.Add(newOrderItem);
    }

    public void Remove(OrderItemId id)
    {
        var itemFoundInOrders = _orderItems.First(x => x.Id == id);
        if (itemFoundInOrders == null)
        {
            throw new DomainException("Order item not found");
        }
        _orderItems.Remove(itemFoundInOrders);
    }
}

public class OrderCreatedEvent : IDomainEvent
{
    public OrderCreatedEvent(Order order)
    {
        throw new NotImplementedException();
    }
}