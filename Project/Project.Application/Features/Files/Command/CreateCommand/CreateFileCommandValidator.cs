using Core.CrossCuttingConcerns.Constant;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Application.Features.Files.Command.CreateCommand
{
    public class CreateFileCommandValidator : AbstractValidator<CreateFileCommand>
    {
        public CreateFileCommandValidator() 
        {
            RuleFor(p => p.UserId)
            .NotEmpty().WithMessage("{PropertyName}" + AspectMessages.IsRequired)
            .NotNull();
        }
    }
}
