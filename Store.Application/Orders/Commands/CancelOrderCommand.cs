using MediatR;
using Store.Domain.Repositories;

namespace Store.Application.Orders.Commands;

public sealed record CancelOrderCommand(Guid OrderId):IRequest<bool>;


public class CancelOrderCommandHandler : IRequestHandler<CancelOrderCommand, bool>
{
    private readonly IOrderRepository _orderRepository;
    private readonly IUnitOfWork _unitOfWork;
    public async Task<bool> Handle(CancelOrderCommand request, CancellationToken cancellationToken)
    {
        var order = await _orderRepository.GetByIdAsync(request.OrderId, cancellationToken);

        if (order == null) return false;

        order.Cancel();

        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return true;
    }
}