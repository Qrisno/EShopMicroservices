namespace Ordering.Domain.ValueObjects;

public record OrderItemId
{
    public Guid Value { get; }
    private OrderItemId(Guid value) => Value = value;

    private  OrderItemId() { }
    public static OrderItemId Of(Guid value)
    {
        if (value == Guid.Empty)
        {
             throw new DomainException("Order Item Id cannot be empty");
        }
        
        return new OrderItemId(value);
    }
}