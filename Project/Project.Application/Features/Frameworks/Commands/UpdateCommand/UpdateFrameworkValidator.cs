using Core.CrossCuttingConcerns.Constant;
using FluentValidation;

namespace Application.Features.Frameworks.Commands.UpdateCommand;

public class UpdateFrameworkValidator:AbstractValidator<UpdateFrameworkCommand>
{
    public UpdateFrameworkValidator()
    {
        RuleFor(p=> p.Id)
            .NotEmpty().WithMessage("{PropertyName}" + AspectMessages.IsRequired)
            .NotNull();
    }
}