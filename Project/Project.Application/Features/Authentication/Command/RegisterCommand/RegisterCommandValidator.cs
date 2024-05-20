using Core.CrossCuttingConcerns.Constant;
using FluentValidation;

namespace Application.Features.Authentication.Command.RegisterCommand;

public class RegisterCommandValidator : AbstractValidator<RegisterCommand>
{
    public RegisterCommandValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("{PropertyName}" + AspectMessages.IsRequired)
            .EmailAddress().WithMessage(AspectMessages.WrongEmailFormat);
        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("{PropertyName}" + AspectMessages.IsRequired);
        RuleFor(x => x.FirstName)
            .NotEmpty().WithMessage("{PropertyName}" + AspectMessages.IsRequired);
        RuleFor(x => x.LastName)
            .NotEmpty().WithMessage("{PropertyName}" + AspectMessages.IsRequired);
    }
}