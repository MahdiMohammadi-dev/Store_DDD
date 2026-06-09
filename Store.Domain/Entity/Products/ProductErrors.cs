namespace Store.Domain.Entity.Products;


public static class ProductErrors
{
    public const string NameIsRequired =
        "Product name is required";

    public const string NameTooLong =
        "Product name is too long";

    public const string InvalidPrice =
        "Price must be greater than zero";

    public const string InvalidStock =
        "Stock cannot be negative";

    public const string InsufficientStock =
        "Stock is insufficient";

    public const string InvalidQuantityValue =
        "InvalidQuantityValue";
}