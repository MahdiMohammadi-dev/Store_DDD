using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Store.Domain.Entity.Customers;

namespace Store.Persistence.Configurations;

public sealed class CustomerConfiguration
    : IEntityTypeConfiguration<Customer>
{
    public void Configure(
        EntityTypeBuilder<Customer> builder)
    {
        builder.ToTable("Customers");

        builder.HasKey(x => x.Id);

        builder.OwnsOne(x => x.Email, email =>
        {
            email.Property(x => x.Value)
                .HasColumnName("Email")
                .HasMaxLength(200);
        });

        builder.OwnsOne(x => x.Address, address =>
        {
            address.Property(x => x.City)
                .HasColumnName("City");

            address.Property(x => x.Street)
                .HasColumnName("Street");

            address.Property(x => x.PostalCode)
                .HasColumnName("PostalCode");
        });
    }
}