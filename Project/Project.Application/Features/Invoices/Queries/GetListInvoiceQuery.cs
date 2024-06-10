using AutoMapper;
using Core.Application.Requests;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Project.Application.Features.Invoices.Dtos.BaseDto;
using Project.Application.Features.Invoices.Models;
using Project.Application.Services.Repositories.Invoices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Application.Features.Invoices.Queries
{
    public class GetListInvoiceQuery : IRequest<InvoiceListModel>
    {
        public PageRequest PageRequest { get; set; }

        public class GetListInvoiceQueryHandler : IRequestHandler<GetListInvoiceQuery, InvoiceListModel>
        {
            private readonly IMapper _mapper;
            private readonly IInvoiceReadRepository _invoiceReadRepository;

            public GetListInvoiceQueryHandler(IInvoiceReadRepository invoiceReadRepository, IMapper mapper)
            {
                _mapper = mapper;
                _invoiceReadRepository = invoiceReadRepository;
            }

            public async Task<InvoiceListModel> Handle(GetListInvoiceQuery request, CancellationToken cancellationToken)
            {
                var invoices = await _invoiceReadRepository.GetListAsync(include: m => m.Include(p => p.InvoiceId),
                    index: request.PageRequest.Page, size: request.PageRequest.PageSize);
                return _mapper.Map<InvoiceListModel>(invoices);
            }
        }
    }
}