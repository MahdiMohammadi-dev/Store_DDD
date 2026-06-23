using MediatR;
using Store.Domain.Entity.Customers;
using Store.Domain.Repositories;
using Store.Domain.ValueObjects;

namespace Store.Application.Customers.Command;

public sealed record RegisterCustomerCommand(string Email,string City,string Street,string PostalCode):IRequest<Guid>;

public sealed class RegisterCustomerCommandHandler
    : IRequestHandler<RegisterCustomerCommand, Guid>
{
    private readonly ICustomerRepository _repository;
    private readonly IUnitOfWork _unitOfWork;

    public RegisterCustomerCommandHandler(ICustomerRepository repository, IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Guid> Handle(
        RegisterCustomerCommand request,
        CancellationToken cancellationToken)
    {
        var customer = new Customer(
            new Email(request.Email),
            new Address(
                request.City,
                request.Street,
                request.PostalCode));

        await _repository.AddAsync(customer, cancellationToken);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return customer.Id;
    }
}