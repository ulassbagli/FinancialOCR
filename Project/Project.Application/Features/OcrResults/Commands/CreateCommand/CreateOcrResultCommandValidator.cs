using Core.CrossCuttingConcerns.Constant;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Application.Features.OcrResults.Commands.CreateCommand
{
    public class CreateOcrResultCommandValidator : AbstractValidator<CreateOcrResultCommand>
    {
        public CreateOcrResultCommandValidator()
        {
            RuleFor(p => p.UserId)
            .NotEmpty().WithMessage("{PropertyName}" + AspectMessages.IsRequired)
            .NotNull();
        }
    }
}
