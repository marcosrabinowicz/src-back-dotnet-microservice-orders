using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Orders.Domain.Aggregates;

namespace Orders.Infrastructure.EF.Configurations;

public class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.ToTable("Orders");

        builder.HasKey(o => o.Id);

        builder.Property(o => o.CustomerId)
            .IsRequired();

        builder.Ignore(o => o.Total); // calculado

        builder.Navigation("_items")
            .UsePropertyAccessMode(PropertyAccessMode.Field);

        builder.HasMany(o => o.Items)
            .WithOne()
            .HasForeignKey("OrderId")
            .OnDelete(DeleteBehavior.Cascade);

        // Índice fundamental:
        builder.HasIndex(o => o.CustomerId);
        builder.HasIndex(o => o.Id).IsUnique();
    }
}
