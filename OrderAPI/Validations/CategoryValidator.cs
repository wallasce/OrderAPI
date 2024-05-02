using FluentValidation;
using OrderAPI.Models;

namespace OrderAPI.Validations;

public class CategoryValidator : AbstractValidator<Category>
{
    public CategoryValidator RuleName()
    {
        RuleFor(c => c.Name)
            .MaximumLength(50)
            .NotEmpty()
            .WithMessage("Category name size should be between 0 and 50");
        return this;
    }

}
