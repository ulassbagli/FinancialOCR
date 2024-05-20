using Core.CrossCuttingConcerns.Constant;
using Domain.Entities;
using FluentValidation;

namespace Application.Features.Frameworks.Commands.CreateCommand;

public class CreateFrameworkValidator:AbstractValidator<CreateFrameworkCommand>
{
    public CreateFrameworkValidator()
    {
        RuleFor(p => p.Name)
            .NotEmpty().WithMessage("{PropertyName}" + AspectMessages.IsRequired)
            .NotNull()
            .MaximumLength(50).WithMessage("{PropertyName} must not exceed 50 characters.");
    }
}