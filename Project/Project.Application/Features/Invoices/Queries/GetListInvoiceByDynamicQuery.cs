using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Core.Application.Requests;
using Core.Persistence.Dynamic;
using Core.Security.Enums;
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
    public class GetListInvoiceByDynamicQuery : IRequest<InvoiceListModel>, ISecuredRequest
    {
        public Dynamic Dynamic { get; set; }
        public PageRequest PageRequest { get; set; }

        public string[] Roles { get; } = { OperationClaimsEnum.Invoice.ToString() };

        public class GetListInvoiceByDynamicQueryHandler : IRequestHandler<GetListInvoiceByDynamicQuery, InvoiceListModel>
        {
            private readonly IInvoiceReadRepository _invoiceReadRepository;
            private readonly IMapper _mapper;

            public GetListInvoiceByDynamicQueryHandler(IMapper mapper, IInvoiceReadRepository invoiceReadRepository)
            {
                _invoiceReadRepository = invoiceReadRepository;
                _mapper = mapper;
            }

            public async Task<InvoiceListModel> Handle(GetListInvoiceByDynamicQuery request, CancellationToken cancellationToken)
            {
                var models = await _invoiceReadRepository.GetListByDynamicAsync(request.Dynamic, include:
                    m => m.Include(c => c.User),
                    index: request.PageRequest.Page,
                    size: request.PageRequest.PageSize,
                    cancellationToken: cancellationToken);
                return _mapper.Map<InvoiceListModel>(models);
            }
        }
    }
}
