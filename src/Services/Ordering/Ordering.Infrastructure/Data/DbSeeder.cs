using Microsoft.EntityFrameworkCore;
using Ordering.Domain.Models;
using Ordering.Domain.ValueObjects;

namespace Ordering.Infrastructure.Data;

public static class DbSeeder
{
    public static async Task SeedAsync(ApplicationDbContext context)
    {
        if (await context.Orders.AnyAsync()) return;

        // Create sample customers
        var customer1 = Customer.Create(
            CustomerId.Of(Guid.NewGuid()),
            "John Doe",
            "john.doe@example.com"
        );

        var customer2 = Customer.Create(
            CustomerId.Of(Guid.NewGuid()),
            "Jane Smith",
            "jane.smith@example.com"
        );

        await context.Customers.AddRangeAsync(customer1, customer2);
    
        // Create sample products
        var product1 = Product.Create(
            "Product 1",
            10.99m
        );

        var product2 = Product.Create(
            "Product 2",
            24.99m
        );

        var product3 = Product.Create(
            "Product 3",
            5.99m
        );

        await context.Products.AddRangeAsync(product1, product2, product3);

        // Create delivery and billing addresses
        var address1 = Address.Of(
            "John", "Doe", "john.doe@example.com", 
            "123 Main St", "New York", "NY", "USA", "10001"
        );

        var address2 = Address.Of(
            "Jane", "Smith", "jane.smith@example.com", 
            "456 Oak Ave", "San Francisco", "CA", "USA", "94102"
        );

        // Create order items
        var order1Items = new List<OrderItem>
        {
            OrderItem.Create(OrderId.Of(Guid.NewGuid()), product1.Id, product1.Price, 2),
            OrderItem.Create(OrderId.Of(Guid.NewGuid()), product2.Id, product2.Price, 1)
        };

        var order2Items = new List<OrderItem>
        {
            OrderItem.Create(OrderId.Of(Guid.NewGuid()), product2.Id, product2.Price, 3),
            OrderItem.Create(OrderId.Of(Guid.NewGuid()), product3.Id, product3.Price, 2)
        };

        // Create orders
        var order1 = Order.Create(
            customer1.Id,
            OrderName.Of("Order #1001"),
            order1Items,
            address1,
            address1
        );

        var order2 = Order.Create(
            customer2.Id,
            OrderName.Of("Order #1002"),
            order2Items,
            address2,
            address2
        );

        // Add orders to context
        await context.Orders.AddRangeAsync(order1, order2);
    
        // Save all changes
        await context.SaveChangesAsync();
    }
}
