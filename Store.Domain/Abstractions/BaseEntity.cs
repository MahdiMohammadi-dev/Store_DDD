namespace Store.Domain.Abstractions;

public class BaseEntity
{
    public Guid Id { get; protected set; }
}