using MediatR;
using Store.Domain.Repositories;

namespace Store.Application.Products.Commands;

public sealed record DecreaseStockCommand(Guid ProductId,int Quantity):IRequest<bool>;


public sealed class DecreaseStockCommandHandler : IRequestHandler<DecreaseStockCommand, bool>
{
    private readonly IProductRepository _productRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DecreaseStockCommandHandler(IProductRepository productRepository, IUnitOfWork unitOfWork)
    {
        _productRepository = productRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<bool> Handle(DecreaseStockCommand request, CancellationToken cancellationToken)
    {
        var product = await _productRepository.GetByIdAsync(request.ProductId, cancellationToken);
        if (product == null) return false;

        product.DecreaseStock(request.Quantity);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return true;
    }
}