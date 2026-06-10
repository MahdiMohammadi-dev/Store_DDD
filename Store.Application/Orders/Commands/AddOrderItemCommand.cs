using MediatR;
using Store.Domain.Repositories;

namespace Store.Application.Orders.Commands;

public sealed record AddOrderItemCommand(Guid OrderId,Guid ProductId,int Quantity):IRequest<bool>;


public sealed class AddOrderItemCommandHandler : IRequestHandler<AddOrderItemCommand, bool>
{
    private readonly IOrderRepository _orderRepository;
    private readonly IProductRepository _productRepository;
    private readonly IUnitOfWork _unitOfWork;


    public AddOrderItemCommandHandler(IOrderRepository orderRepository, IUnitOfWork unitOfWork, IProductRepository productRepository)
    {
        _orderRepository = orderRepository;
        _unitOfWork = unitOfWork;
        _productRepository = productRepository;
    }
    public async Task<bool> Handle(AddOrderItemCommand request, CancellationToken cancellationToken)
    {
        var order = await _orderRepository.GetByIdAsync(request.OrderId, cancellationToken);

        var product = await _productRepository.GetByIdAsync(request.ProductId, cancellationToken);

        if (order == null || product == null)
            return false;

        order.AddItem(product.Id, request.Quantity, product.Price);

        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return true;

    }
}
