namespace Store.Domain.Entity.Products;

public class Product
{
    public Guid Id { get;private set; }
    public string Name { get; private set; }
    public decimal Price { get; private set; }
    public int Stock { get; private set; }


    private Product()
    {
    }

    // Constructor
    public Product(Guid id, string name, decimal price, int stock)
    {

        if(string.IsNullOrWhiteSpace(name))
            throw new Exception("name can not be null");

        if (price < 0 || stock < 0)
            throw new Exception("price or stock can not less than 0");

        Id = Guid.NewGuid();
        Name = name;
        Price = price;
        Stock = stock;
    }





    // Behaviors
    public void ChangePrice(decimal newPrice)
    {
        if (Price < 0)
            throw new Exception("price can not less than 0");

        Price = newPrice;
    }
    public void IncreaseStock(int quantity)
    {
        if (quantity <= 0)
            throw new Exception("quantity can not less than 0");

        Stock += quantity;
    }

    public void DecreaseStock(int quantity)
    {
        if (quantity <= 0)
            throw new Exception("quantity can not less than 0");

        if (Stock < quantity)
            throw new Exception("Invalid data , stock is less than quantity");

        Stock -= quantity;
    }

    public void ChangeProductName(string newProductName)
    {
        if (string.IsNullOrWhiteSpace(newProductName))
            throw new Exception("product name can not be null");
        
        if (newProductName.Length > 40)
            throw new Exception("product name is too long");

        Name = newProductName;
    }

}