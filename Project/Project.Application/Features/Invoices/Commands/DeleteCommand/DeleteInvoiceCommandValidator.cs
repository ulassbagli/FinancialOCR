using Core.CrossCuttingConcerns.Constant;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Application.Features.Invoices.Commands.DeleteCommand
{
    public class DeleteInvoiceCommandValidator : AbstractValidator<DeleteInvoiceCommand>
    {
        public DeleteInvoiceCommandValidator() 
        {
            RuleFor(p => p.Id)
            .NotEmpty().WithMessage("{PropertyName}" + AspectMessages.IsRequired)
            .NotNull();

            RuleFor(p => p.isSoftDelete)
                .NotEmpty().WithMessage("{PropertyName}" + AspectMessages.IsRequired)
                .NotNull();
        }
    }
}
