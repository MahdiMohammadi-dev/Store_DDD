namespace Store.Application.Customers.Dto;

public sealed record CustomerDto(Guid Id, string Email, string City, string Street, string PostalCode);