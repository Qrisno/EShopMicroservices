namespace Ordering.Domain.ValueObjects;

public class OrderName
{
    private const int MAX_LENGTH = 50;
    public string Value { get; } = string.Empty;
    private OrderName(string value) => Value = value;

    private OrderName() { }
    public static OrderName Of(string name)
    {
        ArgumentException.ThrowIfNullOrEmpty(name);
        ArgumentOutOfRangeException.ThrowIfGreaterThan(name.Length, MAX_LENGTH);
        if(name == string.Empty)
            throw new DomainException("Order Name cannot be empty");

        return new OrderName(name);
    }
}