using Core.CrossCuttingConcerns.Constant;
using Domain.Entities;
using FluentValidation;

namespace Application.Features.Frameworks.Commands.DeleteCommand;

public class DeleteFrameworkValidator:AbstractValidator<DeleteFrameworkCommand>
{
    public DeleteFrameworkValidator()
    {
        RuleFor(p=> p.Id)
            .NotEmpty().WithMessage("{PropertyName}" + AspectMessages.IsRequired)
            .NotNull();
        RuleFor(p => p.isSoftDelete)
            .NotEmpty().WithMessage("{PropertyName}" + AspectMessages.IsRequired)
            .NotNull();
    }
}