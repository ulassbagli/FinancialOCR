using Core.CrossCuttingConcerns.Constant;
using FluentValidation;

namespace Application.Features.Authentication.Command.UserOperationClaimCommand;

public class CreateUserOperationClaimCommandValidator:AbstractValidator<CreateUserOperationClaimCommand>
{
    public CreateUserOperationClaimCommandValidator()
    {
        RuleFor(x=>x.User)
            .NotEmpty().WithMessage("{PropertyName}" + AspectMessages.IsRequired)
            .NotNull().WithMessage("{PropertyName}" + AspectMessages.NotFound);
        RuleFor(x=>x.OperationClaims)
            .NotEmpty().WithMessage("{PropertyName}" +AspectMessages.IsRequired)
            .NotNull().WithMessage("{PropertyName}" + AspectMessages.NotFound)
            .Must(x=>x.Count>0).WithMessage("{PropertyName}" + AspectMessages.MustBeGreaterThanZero);
    }
}