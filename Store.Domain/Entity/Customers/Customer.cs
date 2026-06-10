using Store.Domain.ValueObjects;

namespace Store.Domain.Entity.Customers;

public class Customer
{
    public Guid Id { get; private set; }
    public Email Email { get; private set; }

    public Address Address { get; private set; }

    public Customer(Email email, Address address)
    {
        Email = email;
        Address = address;
    }

    public void ChangeAddress(Address address)
    {
        Address = address;
    }
}