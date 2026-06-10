using MediatR;
using Store.Domain.Repositories;
using Store.Domain.ValueObjects;

namespace Store.Application.Products.Commands;

public sealed record ChangePriceCommand(Guid ProductId,decimal Amount , string Currency) : IRequest<bool>;

public sealed class ChangePriceCommandHandler:IRequestHandler<ChangePriceCommand,bool>
{
    private readonly IProductRepository _productRepository;
    private readonly IUnitOfWork _unitOfWork;

    public ChangePriceCommandHandler(IProductRepository productRepository, IUnitOfWork unitOfWork)
    {
        _productRepository = productRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<bool> Handle(ChangePriceCommand request, CancellationToken cancellationToken)
    {
        var product = await _productRepository.GetByIdAsync(request.ProductId, cancellationToken);
        if (product == null) return false;

        product.ChangePrice(new Money(request.Amount,request.Currency));

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return true;
    }
}



public sealed record DeactivateProductCommand(Guid ProductId):IRequest<bool>;

public sealed class DeactivateProductCommandHandler:IRequestHandler<DeactivateProductCommand,bool>
{
    private readonly IProductRepository _productRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeactivateProductCommandHandler(IProductRepository productRepository, IUnitOfWork unitOfWork)
    {
        _productRepository = productRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<bool> Handle(DeactivateProductCommand request, CancellationToken cancellationToken)
    {
        var product = await _productRepository.GetByIdAsync(request.ProductId, cancellationToken);
        if (product == null) return false;

        product.DeActiveProduct();
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return true;

    }
}