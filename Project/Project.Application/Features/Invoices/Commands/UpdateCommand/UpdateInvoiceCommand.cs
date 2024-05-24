using AutoMapper;
using Domain.Entities;
using MediatR;
using Project.Application.Features.Invoices.Dtos.BaseDto;
using Project.Application.Features.Invoices.Rules;
using Project.Application.Services.Repositories.Invoices;
using Project.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Application.Features.Invoices.Commands.UpdateCommand
{
    public class UpdateInvoiceCommand : IRequest<BaseInvoiceDto>
    {
        public string Id { get; set; }
        public string UserId { get; set; }

        public class UpdateInvoiceCommandHandler : IRequestHandler<UpdateInvoiceCommand, BaseInvoiceDto>
        {
            private readonly IInvoiceWriteRepository _invoiceWriteRepository;
            private readonly InvoiceBusinessRules _invoiceBusinessRules;
            private readonly IMapper _mapper;

            public UpdateInvoiceCommandHandler(IMapper mapper, IInvoiceWriteRepository invoiceWriteRepository, InvoiceBusinessRules invoiceBusinessRules)
            {
                _invoiceWriteRepository = invoiceWriteRepository;
                _invoiceBusinessRules = invoiceBusinessRules;
                _mapper = mapper;
            }

            public async Task<BaseInvoiceDto> Handle(UpdateInvoiceCommand request, CancellationToken cancellationToken)
            {
                var invoiceToUpdate = await _invoiceBusinessRules.CheckIfInvoiceDoesNotExistsAndGetInvoice(request.Id);
                await _invoiceBusinessRules.CheckIfUserDoesNotExistsAndGetUser(request.UserId);

                _mapper.Map(request, invoiceToUpdate, typeof(UpdateInvoiceCommand), typeof(Invoice));
                await _invoiceWriteRepository.Update(invoiceToUpdate);
                return _mapper.Map<BaseInvoiceDto>(invoiceToUpdate);
            }
        }
    }
}
