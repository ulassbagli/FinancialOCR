using Core.CrossCuttingConcerns.Constant;
using FluentValidation;

namespace Application.Features.SocialMedias.Commands.DeleteCommand;

public class DeleteSocialMediaCommandValidator: AbstractValidator<DeleteSocialMediaCommand>
{
    public DeleteSocialMediaCommandValidator()
    {
        RuleFor(p=> p.Id)
            .NotEmpty().WithMessage("{PropertyName}" + AspectMessages.IsRequired)
            .NotNull();
        RuleFor(p => p.isSoftDelete)
            .NotEmpty().WithMessage("{PropertyName}" + AspectMessages.IsRequired)
            .NotNull();
    }
}