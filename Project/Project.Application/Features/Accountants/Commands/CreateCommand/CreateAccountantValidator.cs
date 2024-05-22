using Core.CrossCuttingConcerns.Constant;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Application.Features.Accountants.Commands.CreateCommand
{
    public class CreateAccountantValidator : AbstractValidator<CreateAccountantCommand>
    {
        public CreateAccountantValidator() 
        {
            RuleFor(p => p.AccountantId)
                .NotEmpty().WithMessage("{PropertyName}" + AspectMessages.IsRequired)
                .NotNull();
        }
    }
}
