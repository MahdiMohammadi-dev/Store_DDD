using MediatR;
using Store.Domain.Repositories;
using Store.Domain.ValueObjects;

namespace Store.Application.Products.Commands;

public sealed record ChangeProductNameCommand(Guid ProductId,string ProductName):IRequest<bool>;


public sealed class ChangeProductNameCommandHandler:IRequestHandler<ChangeProductNameCommand,bool>
{
    private readonly IProductRepository _productRepository;
    private readonly IUnitOfWork _unitOfWork;

    public ChangeProductNameCommandHandler(IUnitOfWork unitOfWork, IProductRepository productRepository)
    {
        _unitOfWork = unitOfWork;
        _productRepository = productRepository;
    }

    public async Task<bool> Handle(ChangeProductNameCommand request, CancellationToken cancellationToken)
    {
        var product = await _productRepository.GetByIdAsync(request.ProductId, cancellationToken);

        if (product == null) return false;

        product.ChangeProductName(new ProductName(request.ProductName));

       await _unitOfWork.SaveChangesAsync(cancellationToken);

       return true;
    }
}