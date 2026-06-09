namespace Store.Domain.ValueObjects;

public sealed record Email
{
    public string Value { get;}


    public Email(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new Exception("Email is required");

        if (!value.Contains("@"))
            throw new Exception("Invalid email");

        Value = value;
    }
}