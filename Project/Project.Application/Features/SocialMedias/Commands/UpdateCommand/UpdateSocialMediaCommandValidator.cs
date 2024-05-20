using Core.CrossCuttingConcerns.Constant;
using FluentValidation;

namespace Application.Features.SocialMedias.Commands.UpdateCommand;

public class UpdateSocialMediaCommandValidator: AbstractValidator<UpdateSocialMediaCommand>
{
    public UpdateSocialMediaCommandValidator()
    {
        RuleFor(p=> p.Id)
            .NotEmpty().WithMessage("{PropertyName}" + AspectMessages.IsRequired)
            .NotNull();
    }
}