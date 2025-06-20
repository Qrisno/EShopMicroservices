

namespace Ordering.Domain.Models;

public class Product: Entity<ProductId>
{
    public string Name { get; private set; } = string.Empty;
    public decimal Price { get; private set; }

    public static Product Create(string name, decimal price)
    {
        ArgumentException.ThrowIfNullOrEmpty(name);
        ArgumentOutOfRangeException.ThrowIfLessThan(price, 0);
        return new Product
        {
            Name = name,
            Price = price
        };
    }
}