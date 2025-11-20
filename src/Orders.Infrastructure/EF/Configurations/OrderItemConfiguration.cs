using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Orders.Domain.Entities;

namespace Orders.Infrastructure.EF.Configurations;

public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
{
    public void Configure(EntityTypeBuilder<OrderItem> builder)
    {
        builder.ToTable("OrderItems");

        builder.HasKey(o => new { o.ProductId, o.Quantity, o.UnitPrice, });

        builder.Property(o => o.ProductId).IsRequired();
        builder.Property(o => o.Quantity).IsRequired();
        builder.Property(o => o.UnitPrice).IsRequired();

        builder.Property<decimal>("Total")
            .HasComputedColumnSql("[Quantity] * [UnitPrice]");
    }
}