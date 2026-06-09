using FluentValidation;

namespace Store.Application.Products.Commands;

public class CreateProductCommandValidator:AbstractValidator<CreateProductCommand>
{
    public CreateProductCommandValidator()
    {

        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("The product name can not be null")
            .MaximumLength(40)
            .WithMessage("product name is too long");

        RuleFor(x => x.Price)
            .GreaterThan(0);

        RuleFor(x => x.Stock)
            .GreaterThanOrEqualTo(0);
    }
}