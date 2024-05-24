using Core.CrossCuttingConcerns.Constant;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Application.Features.SubscriptionPayments.Command.UpdateCommand
{
    public class UpdateSubscriptionPaymentCommandValidator : AbstractValidator<UpdateSubscriptionPaymentCommand>
    {
        public UpdateSubscriptionPaymentCommandValidator() 
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
