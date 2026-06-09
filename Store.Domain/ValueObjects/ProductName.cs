using Store.Domain.Entity.Products;

namespace Store.Domain.ValueObjects;

public sealed record ProductName
{
    public string Value { get; }

    public ProductName(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ProductException(ProductErrors.NameIsRequired);

        if (value.Length > 40)
            throw new ProductException(ProductErrors.NameTooLong);

        Value = value;
    }
}