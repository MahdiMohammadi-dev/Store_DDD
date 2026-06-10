using MediatR;
using Store.Domain.Repositories;

namespace Store.Application.Orders.Commands;

public sealed record PayOrderCommand(Guid OrderId):IRequest<bool>;



public class PayOrderCommandHandler : IRequestHandler<PayOrderCommand, bool>
{

    private readonly IOrderRepository _orderRepository;
    private readonly IUnitOfWork _unitOfWork;

    public PayOrderCommandHandler(IOrderRepository orderRepository, IProductRepository productRepository, IUnitOfWork unitOfWork)
    {
        _orderRepository = orderRepository;
        _unitOfWork = unitOfWork;
    }


    public async Task<bool> Handle(PayOrderCommand request, CancellationToken cancellationToken)
    {
        var order = await _orderRepository.GetByIdAsync(request.OrderId, cancellationToken);
        if (order == null) return false;

        order.Pay();
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return true;
    }
}

