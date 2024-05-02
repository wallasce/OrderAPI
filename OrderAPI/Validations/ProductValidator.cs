using FluentValidation;
using OrderAPI.Models;

namespace OrderAPI.Validations;

public class ProductValidator : AbstractValidator<Product>
{
    public ProductValidator RuleCategoryId()
    {
        RuleFor(p => p.CategoryId)
            .NotEmpty()
            .GreaterThan(0)
            .WithMessage("Product have be an category");
        return this;
    }

    public ProductValidator RuleName()
    {
        RuleFor(p => p.Name)
            .NotEmpty()
            .MaximumLength(50)
            .WithMessage("Size of product name should be between 0 and 50");
        return this;
    }

    public ProductValidator RuleDescription() {
        RuleFor(p => p.Description)
            .NotEmpty()
            .MaximumLength(200)
            .WithMessage("Descriprion text size should be between 0 and 200");
        return this;
    }
    public ProductValidator RulePrice()
    {
        RuleFor(p => p.Price)
            .GreaterThanOrEqualTo(0)
            .WithMessage("Price should be positive");
        return this;
    }

    public ProductValidator RuleServes()
    {
        RuleFor(p => p.Serves)
            .GreaterThan(0)
            .WithMessage("Product should serve at least one people");
        return this;
    }
}
