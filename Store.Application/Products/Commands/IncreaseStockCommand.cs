using MediatR;
using Store.Domain.Repositories;

namespace Store.Application.Products.Commands;

public sealed record IncreaseStockCommand(Guid ProductId,int Quantity):IRequest<bool>;


public sealed class IncreaseStockCommandHandler : IRequestHandler<IncreaseStockCommand, bool>
{
    private readonly IProductRepository _productRepository;
    private readonly IUnitOfWork _unitOfWork;

    public IncreaseStockCommandHandler(IProductRepository productRepository, IUnitOfWork unitOfWork)
    {
        _productRepository = productRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<bool> Handle(IncreaseStockCommand request, CancellationToken cancellationToken)
    {
        var product = await _productRepository.GetByIdAsync(request.ProductId, cancellationToken);
        if (product == null) return false;

        product.IncreaseStock(request.Quantity);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return true;
    }
}