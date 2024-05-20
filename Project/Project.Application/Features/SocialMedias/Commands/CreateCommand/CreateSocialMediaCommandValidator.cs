using Core.CrossCuttingConcerns.Constant;
using FluentValidation;

namespace Application.Features.SocialMedias.Commands.CreateCommand;

public class CreateSocialMediaCommandValidator : AbstractValidator<CreateSocialMediaCommand>
{
    public CreateSocialMediaCommandValidator()
    {
        RuleFor(p=> p.DeveloperId)
            .NotEmpty().WithMessage("{PropertyName}" + AspectMessages.IsRequired)
            .NotNull();
        RuleFor(p=> p.Name)
            .NotEmpty().WithMessage("{PropertyName}" + AspectMessages.IsRequired)
            .NotNull()
            .MaximumLength(50).WithMessage("{PropertyName} must not exceed 50 characters.");
        RuleFor(p=> p.Url)
            .NotEmpty().WithMessage("{PropertyName}" + AspectMessages.IsRequired)
            .NotNull()
            .MaximumLength(200).WithMessage("{PropertyName} must not exceed 200 characters.");
    }
}