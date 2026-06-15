using Store.Domain.Entity.Products;

namespace Store.Persistence.Configurations;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public sealed class ProductConfiguration
    : IEntityTypeConfiguration<Product>
{
    public void Configure(
        EntityTypeBuilder<Product> builder)
    {
        builder.ToTable("Products");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id);

        builder.Property(x => x.Stock)
            .IsRequired();

        builder.OwnsOne(x => x.Name, name =>
        {
            name.Property(p => p.Value)
                .HasColumnName("Name")
                .HasMaxLength(100)
                .IsRequired();
        });

        builder.OwnsOne(x => x.Price, price =>
        {
            price.Property(p => p.Amount)
                .HasColumnName("Price")
                .HasPrecision(18, 2);

            price.Property(p => p.Currency)
                .HasColumnName("Currency")
                .HasMaxLength(10);
        });
    }
}