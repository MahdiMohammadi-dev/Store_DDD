namespace Store.Domain.ValueObjects;

public sealed record Money
{
    public decimal Amount { get; }

    public string Currency { get; }

    public Money(
        decimal amount,
        string currency)
    {
        if (amount < 0)
            throw new Exception("Invalid Amount");

        if (string.IsNullOrWhiteSpace(currency))
            throw new Exception("currency can not be null");

        Amount = amount;

        Currency = currency;
    }
}