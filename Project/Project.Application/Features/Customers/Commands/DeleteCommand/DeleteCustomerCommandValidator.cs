using Core.CrossCuttingConcerns.Constant;
using FluentValidation;
using Project.Application.Features.Customers.Commands.CreateCommand;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Application.Features.Customers.Commands.DeleteCommand
{
    public class DeleteCustomerCommandValidator : AbstractValidator<DeleteCustomerCommand>
    {
        public DeleteCustomerCommandValidator() 
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
