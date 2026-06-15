using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Store.Domain.Entity.Orders;

namespace Store.Persistence.Configurations;

public sealed class OrderConfiguration
    : IEntityTypeConfiguration<Order>
{
    public void Configure(
        EntityTypeBuilder<Order> builder)
    {
        builder.ToTable("Orders");

        builder.HasKey(x => x.Id);

        builder.OwnsMany(x => x.Items, item =>
        {
            item.ToTable("OrderItems");

            item.WithOwner()
                .HasForeignKey("OrderId");

            item.Property<Guid>("Id");

            item.HasKey("Id");

            item.Property(x => x.ProductId);

            item.Property(x => x.Quantity);

            item.OwnsOne(x => x.UnitPrice, price =>
            {
                price.Property(p => p.Amount)
                    .HasColumnName("UnitPrice");

                price.Property(p => p.Currency)
                    .HasColumnName("Currency");
            });
        });
    }
}