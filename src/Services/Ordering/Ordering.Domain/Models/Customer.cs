namespace Ordering.Domain.Models;

public class Customer: Entity<CustomerId>
{
    public string Name { get; private set; } = string.Empty;
    public string Email { get; private set; } = string.Empty;

    private Customer()
    {
        
    }
    public static Customer Create(CustomerId id, string name, string email)
    {
        ArgumentException.ThrowIfNullOrEmpty(name);
        ArgumentException.ThrowIfNullOrEmpty(email);
        return new Customer
        {
            Id = id,
            Name = name,
            Email = email
        };
    }
}