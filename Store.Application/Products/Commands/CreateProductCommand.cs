using MediatR;
using Store.Domain.Entity.Products;
using Store.Domain.Repositories;
using Store.Domain.ValueObjects;

namespace Store.Application.Products.Commands;

public sealed record CreateProductCommand(string Name,decimal Price,int Stock):IRequest<Guid>;


public class CreateProductCommandHandler :IRequestHandler<CreateProductCommand,Guid>
{
    private readonly IProductRepository _productRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateProductCommandHandler(IProductRepository productRepository, IUnitOfWork unitOfWork)
    {
        _productRepository = productRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Guid> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var product = new Product(new ProductName(request.Name), request.Stock,new Money(request.Price,"IR"));

        await _productRepository.AddAsync(product, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);


        return product.Id;
    }
}