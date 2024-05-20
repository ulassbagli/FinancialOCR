using Core.CrossCuttingConcerns.Constant;
using FluentValidation;

namespace Application.Features.Developers.Commands.CreateCommand;

public class CreateDeveloperCommandValidator : AbstractValidator<CreateDeveloperCommand>
{
    public CreateDeveloperCommandValidator()
    {
        RuleFor(p=> p.UserId)
            .NotEmpty().WithMessage("{PropertyName}" + AspectMessages.IsRequired)
            .NotNull();
    }
}