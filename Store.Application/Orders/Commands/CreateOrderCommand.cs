using MediatR;
using Store.Domain.Entity.Orders;
using Store.Domain.Repositories;

namespace Store.Application.Orders.Commands;

public sealed record CreateOrderCommand(Guid CustomerId):IRequest<Guid>;

public sealed class CreateOrderCommandHandler:IRequestHandler<CreateOrderCommand,Guid>
{
    private readonly IOrderRepository _orderRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateOrderCommandHandler(IOrderRepository orderRepository, IUnitOfWork unitOfWork)
    {
        _orderRepository = orderRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Guid> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        var order = new Order(request.CustomerId);
        await _orderRepository.AddAsync(order,cancellationToken);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return order.Id;
    }
}
