using Core.CrossCuttingConcerns.Constant;
using FluentValidation;

namespace Application.Features.ProgrammingLanguages.Commands.UpdateCommand;

public class UpdateProgrammingLanguageCommandValidator: AbstractValidator<UpdateProgrammingLanguageCommand>
{
    public UpdateProgrammingLanguageCommandValidator()
    {
        RuleFor(p=> p.Id)
            .NotEmpty().WithMessage("{PropertyName}" + AspectMessages.IsRequired)
            .NotNull();
        RuleFor(p=> p.Name)
            .NotEmpty().WithMessage("{PropertyName}" + AspectMessages.IsRequired)
            .NotNull()
            .MaximumLength(50).WithMessage("{PropertyName} must not exceed 50 characters.");
    }
}