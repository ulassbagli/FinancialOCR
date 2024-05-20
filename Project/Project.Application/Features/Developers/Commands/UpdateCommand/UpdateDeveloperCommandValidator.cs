using Core.CrossCuttingConcerns.Constant;
using FluentValidation;

namespace Application.Features.Developers.Commands.UpdateCommand;

public class UpdateDeveloperCommandValidator: AbstractValidator<UpdateDeveloperCommand>
{
    public UpdateDeveloperCommandValidator()
    {
        RuleFor(p=> p.Id)
            .NotEmpty().WithMessage("{PropertyName}" + AspectMessages.IsRequired)
            .NotNull();
        RuleFor(p=> p.UserId)
            .NotEmpty().WithMessage("{PropertyName}" + AspectMessages.IsRequired)
            .NotNull();
    }
}