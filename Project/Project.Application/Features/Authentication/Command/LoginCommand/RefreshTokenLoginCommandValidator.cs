using Core.CrossCuttingConcerns.Constant;
using FluentValidation;

namespace Application.Features.Authentication.Command.LoginCommand;

public class RefreshTokenLoginCommandValidator: AbstractValidator<RefreshTokenLoginCommand>
{
    public RefreshTokenLoginCommandValidator()
    {
        RuleFor(x => x.AccessToken)
            .NotEmpty().WithMessage("{PropertyName}" + AspectMessages.IsRequired);
        RuleFor(x => x.RefreshToken)
            .NotEmpty().WithMessage("{PropertyName}" + AspectMessages.IsRequired);
    }
}