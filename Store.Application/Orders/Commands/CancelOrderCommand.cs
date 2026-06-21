using MediatR;
using Store.Domain.Repositories;

namespace Store.Application.Orders.Commands;

public sealed record CancelOrderCommand(Guid OrderId):IRequest<bool>;


public class CancelOrderCommandHandler : IRequestHandler<CancelOrderCommand, bool>
{
    private readonly IOrderRepository _orderRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IProductRepository _productRepository;

    public CancelOrderCommandHandler(IOrderRepository orderRepository, IUnitOfWork unitOfWork, IProductRepository productRepository)
    {
        _orderRepository = orderRepository;
        _unitOfWork = unitOfWork;
        _productRepository = productRepository;
    }

    public async Task<bool> Handle(CancelOrderCommand request, CancellationToken cancellationToken)
    {
        var order = await _orderRepository.GetByIdAsync(request.OrderId, cancellationToken);

        if (order == null) return false;

        order.Cancel();

        foreach (var orderItem in order.Items)
        {
            var product = await _productRepository.GetByIdAsync(orderItem.ProductId,cancellationToken);

            product.IncreaseStock(orderItem.Quantity);
        }

        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return true;
    }
}