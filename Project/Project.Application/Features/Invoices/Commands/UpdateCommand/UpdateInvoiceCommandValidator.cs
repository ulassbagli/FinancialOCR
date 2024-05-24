using Core.CrossCuttingConcerns.Constant;
using FluentValidation;
using Project.Application.Features.Invoices.Dtos.BaseDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Application.Features.Invoices.Commands.UpdateCommand
{
    public class UpdateInvoiceCommandValidator : AbstractValidator<UpdateInvoiceCommand>
    {
        public UpdateInvoiceCommandValidator() 
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
