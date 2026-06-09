namespace Store.Domain.ValueObjects;

public sealed record Address
{
    public string City { get; }

    public string Street { get; }

    public string PostalCode { get; }

    public Address(
        string city,
        string street,
        string postalCode)
    {

        if (string.IsNullOrWhiteSpace(city))
            throw new Exception("City can not be null");

        if (string.IsNullOrWhiteSpace(street))
            throw new Exception("street can not be null");

        if (string.IsNullOrWhiteSpace(postalCode))
            throw new Exception("postal code can not be null");

        City = city;
        Street = street;
        PostalCode = postalCode;
    }


}