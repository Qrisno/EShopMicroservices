using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ordering.Domain.Enums;
using Ordering.Domain.Models;
using Ordering.Domain.ValueObjects;

namespace Ordering.Infrastructure.Data.Configurations;

public class OrderConfiguration: IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.HasKey(o => o.Id);
        builder.Property(o => o.Id).HasConversion(
            orderId => orderId.Value,
            dbId => OrderId.Of(dbId)
        );
        builder.Property(o => o.CustomerId).HasConversion(
                customerId=> customerId.Value,
                dbId => CustomerId.Of(dbId)
            );
        
        builder.Property(o => o.OrderName).HasConversion(
            orderName=> orderName.Value,
            dbName => OrderName.Of(dbName)
        );

        builder.HasMany(o => o.OrderItems)
            .WithOne()
            .HasForeignKey("OrderId")
            .OnDelete(DeleteBehavior.Cascade);
       
        builder.OwnsOne(o => o.BillingAddress, addressBuilder =>
        {
            addressBuilder.Property(a => a.FirstName).HasColumnName("BillingFirstName");
            addressBuilder.Property(a => a.LastName).HasColumnName("BillingLastName");
            addressBuilder.Property(a => a.Email).HasColumnName("BillingEmail");
            addressBuilder.Property(a => a.AddressLine).HasColumnName("BillingAddressLine");
            addressBuilder.Property(a => a.City).HasColumnName("BillingCity");
            addressBuilder.Property(a => a.State).HasColumnName("BillingState");
            addressBuilder.Property(a => a.Country).HasColumnName("BillingCountry");
            addressBuilder.Property(a => a.ZipCode).HasColumnName("BillingZipCode");
        
       
            addressBuilder.Property(a => a.AddressLine).IsRequired().HasMaxLength(100);
            addressBuilder.Property(a => a.ZipCode).IsRequired().HasMaxLength(10);

        });builder.OwnsOne(o => o.DeliveryAddress, addressBuilder =>
        {
            addressBuilder.Property(a => a.FirstName).HasColumnName("DeliveryFirstName");
            addressBuilder.Property(a => a.LastName).HasColumnName("DeliveryLastName");
            addressBuilder.Property(a => a.Email).HasColumnName("DeliveryEmail");
            addressBuilder.Property(a => a.AddressLine).HasColumnName("DeliveryAddressLine");
            addressBuilder.Property(a => a.City).HasColumnName("DeliveryCity");
            addressBuilder.Property(a => a.State).HasColumnName("DeliveryState");
            addressBuilder.Property(a => a.Country).HasColumnName("DeliveryCountry");
            addressBuilder.Property(a => a.ZipCode).HasColumnName("DeliveryZipCode");
        
           
            addressBuilder.Property(a => a.AddressLine).IsRequired().HasMaxLength(100);
            addressBuilder.Property(a => a.ZipCode).IsRequired().HasMaxLength(10);

        });

        builder.OwnsOne(o => o.Payment);
        builder.Property(o => o.Status).HasConversion(status => status.ToString(),
            dbStatus => (OrderStatus)Enum.Parse(typeof(OrderStatus), dbStatus));

        builder.ToTable(tb => tb.HasCheckConstraint("CK_Order_Status", "Status IN (1,2,3,4,5)"));

    }
}