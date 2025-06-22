using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ordering.Domain.Models;
using Ordering.Domain.ValueObjects;

namespace Ordering.Infrastructure.Data.Configurations;

public class OrderItemConfiguration:IEntityTypeConfiguration<OrderItem>
{
    public void Configure(EntityTypeBuilder<OrderItem> builder)
    {
        builder.HasKey(o => o.Id);
        builder.Property(o => o.Id).HasConversion(orderItemId =>

                orderItemId.Value,
            dbId => OrderItemId.Of(dbId)
        );
        builder.Property(o => o.ProductId).HasConversion(
            productId => productId.Value,
            dbId => ProductId.Of(dbId)
        );
        builder.Property(o=>o.Price).HasColumnType("decimal(18,2)");
    }
}