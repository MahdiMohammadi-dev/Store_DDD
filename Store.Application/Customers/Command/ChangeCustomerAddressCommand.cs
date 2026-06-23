using MediatR;
using Store.Domain.Repositories;
using Store.Domain.ValueObjects;

namespace Store.Application.Customers.Command;

public sealed record ChangeCustomerAddressCommand(Guid CustomerId, string City, string Street, string PostalCode):IRequest<bool>;


public sealed class ChangeCustomerAddressCommandHandler:IRequestHandler<ChangeCustomerAddressCommand,bool>
{
    private readonly ICustomerRepository _customerRepository;
    private readonly IUnitOfWork _unitOfWork;

    public ChangeCustomerAddressCommandHandler(ICustomerRepository customerRepository, IUnitOfWork unitOfWork)
    {
        _customerRepository = customerRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<bool> Handle(ChangeCustomerAddressCommand request, CancellationToken cancellationToken)
    {
        var customer = await _customerRepository.GetByIdAsync(request.CustomerId,cancellationToken);
        if (customer == null) return false;

        customer.ChangeAddress(new Address(request.City,request.Street,request.PostalCode));

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return true;
    }
}


