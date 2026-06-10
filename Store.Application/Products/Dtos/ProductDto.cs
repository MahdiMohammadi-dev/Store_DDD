namespace Store.Application.Products.Dtos;

public sealed record ProductDto(Guid Id,string Name,decimal Price,int Stock);