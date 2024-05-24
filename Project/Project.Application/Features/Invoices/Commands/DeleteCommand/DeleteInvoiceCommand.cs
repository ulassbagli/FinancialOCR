using AutoMapper;
using Domain.Entities;
using MediatR;
using Project.Application.Features.Invoices.Dtos;
using Project.Application.Features.Invoices.Dtos.BaseDto;
using Project.Application.Features.Invoices.Rules;
using Project.Application.Services.Repositories.Invoices;
using Project.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Application.Features.Invoices.Commands.DeleteCommand
{
    public class DeleteInvoiceCommand : IRequest<DeletedInvoiceDto>
    {
        public string Id { get; set; }
        public bool isSoftDelete { get; set; }

        public class DeleteInvoiceCommandHandler : IRequestHandler<DeleteInvoiceCommand, DeletedInvoiceDto>
        {
            private readonly IInvoiceWriteRepository _invoiceWriteRepository;
            private readonly IMapper _mapper;
            private readonly InvoiceBusinessRules _invoiceBusinessRules;

            public DeleteInvoiceCommandHandler(IInvoiceWriteRepository invoiceWriteRepository, IMapper mapper,
                InvoiceBusinessRules invoiceBusinessRules)
            {
                _mapper = mapper;
                _invoiceBusinessRules = invoiceBusinessRules;
                _invoiceWriteRepository = invoiceWriteRepository;
            }

            public async Task<DeletedInvoiceDto> Handle(DeleteInvoiceCommand request, CancellationToken cancellationToken)
            {
                var invoice = await _invoiceBusinessRules.CheckIfInvoiceDoesNotExistsAndGetInvoice(request.Id);

                Invoice deletedInvoice;
                if (request.isSoftDelete)
                    deletedInvoice = await _invoiceWriteRepository.SoftRemove(invoice);
                else
                    deletedInvoice = await _invoiceWriteRepository.HardRemove(invoice);

                return _mapper.Map<DeletedInvoiceDto>(deletedInvoice);
            }
        }
    }
}
