using Core.CrossCuttingConcerns.Constant;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Application.Features.SubscriptionPayments.Command.CreateCommand
{
    public class CreateSubscriptionPaymentCommandValidator : AbstractValidator<CreateSubscriptionPaymentCommand>
    {
        public CreateSubscriptionPaymentCommandValidator() 
        {
            RuleFor(p => p.UserId)
            .NotEmpty().WithMessage("{PropertyName}" + AspectMessages.IsRequired)
            .NotNull();
        }
    }
}
