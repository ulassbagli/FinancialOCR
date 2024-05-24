using AutoMapper;
using MediatR;
using Project.Application.Features.Invoices.Dtos.BaseDto;
using Project.Application.Features.Invoices.Rules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Application.Features.Invoices.Queries
{
    public class GetByIdInvoiceQuery : IRequest<BaseInvoiceDto>
    {
        public string Id { get; set; }

        public class GetByIdInvoiceQueryHandler : IRequestHandler<GetByIdInvoiceQuery, BaseInvoiceDto>
        {
            private readonly InvoiceBusinessRules _invoiceBusinessRules;
            private readonly IMapper _mapper;

            public GetByIdInvoiceQueryHandler(IMapper mapper, InvoiceBusinessRules invoiceBusinessRules)
            {
                _invoiceBusinessRules = invoiceBusinessRules;
                _mapper = mapper;
            }

            public async Task<BaseInvoiceDto> Handle(GetByIdInvoiceQuery request, CancellationToken cancellationToken)
            {
                var invoice = await _invoiceBusinessRules.CheckIfInvoiceDoesNotExistsAndGetInvoice(request.Id);

                return _mapper.Map<BaseInvoiceDto>(invoice);
            }
        }
    }
}
