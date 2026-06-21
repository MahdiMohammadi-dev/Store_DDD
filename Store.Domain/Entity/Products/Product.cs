using Store.Domain.Abstractions;
using Store.Domain.Entity.Products.Events;
using Store.Domain.ValueObjects;

namespace Store.Domain.Entity.Products;

public class Product :AggregateRoot
{
    public ProductName Name { get; private set; }
    public Money Price { get; private set; }
    public int Stock { get; private set; }

    public bool IsActive { get; private set; } = true;

    private Product()
    {
    }

    // Constructor
    public Product(ProductName name, int stock,Money price)
    {
        if (stock < 0)
            throw new ProductException(ProductErrors.InvalidStock);

        Id = Guid.NewGuid();
        Name = name;
        Stock = stock;
        Price = price;

        RaiseDomainEvent(new ProductCreatedDomainEvent(Guid.NewGuid(), Id));
    }





    // Behaviors
    public void ChangePrice(Money newPrice)
    {
        Price = newPrice;
    }
    public void IncreaseStock(int quantity)
    {
        if (quantity <= 0)
            throw new ProductException(ProductErrors.InvalidStock);

        Stock += quantity;
    }

    public void DecreaseStock(int quantity)
    {
        if (quantity <= 0)
            throw new ProductException(ProductErrors.InvalidQuantityValue);

        if (Stock < quantity)
            throw new ProductException(ProductErrors.InsufficientStock);

        Stock -= quantity;
    }

    public void ChangeProductName(ProductName newProductName)
    {
        Name = newProductName;
    }

    public void DeActiveProduct()
    {
        IsActive = false;
    }
}