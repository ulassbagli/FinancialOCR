using Core.CrossCuttingConcerns.Constant;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Application.Features.Files.Command.UpdateCommand
{
    public class UpdateFileCommandValidator : AbstractValidator<UpdateFileCommand>
    {
        public UpdateFileCommandValidator() 
        {
            RuleFor(p => p.Id)
            .NotEmpty().WithMessage("{PropertyName}" + AspectMessages.IsRequired)
            .NotNull();
            RuleFor(p => p.UserId)
                .NotEmpty().WithMessage("{PropertyName}" + AspectMessages.IsRequired)
                .NotNull();
        }
    }
}
