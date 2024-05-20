using Core.CrossCuttingConcerns.Constant;
using FluentValidation;

namespace Application.Features.Authentication.Command.LoginCommand;

public class LoginCommandValidator : AbstractValidator<LoginCommand>
{
    public LoginCommandValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("{PropertyName}" + AspectMessages.IsRequired)
            .EmailAddress().WithMessage(AspectMessages.WrongEmailFormat);
        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("{PropertyName}" + AspectMessages.IsRequired);
    }
}