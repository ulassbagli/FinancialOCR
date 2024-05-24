using AutoMapper;
using Core.Application.Requests;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Project.Application.Features.Customers.Models;
using Project.Application.Services.Repositories.Customers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Application.Features.Customers.Queries
{
    public class GetListCustomerQuery : IRequest<CustomerListModel>
    {
        public PageRequest PageRequest { get; set; }

        public class GetListCustomerQueryHandler : IRequestHandler<GetListCustomerQuery, CustomerListModel>
        {
            private readonly IMapper _mapper;
            private readonly ICustomerReadRepository _customerReadRepository;

            public GetListCustomerQueryHandler(ICustomerReadRepository customerReadRepository, IMapper mapper)
            {
                _mapper = mapper;
                _customerReadRepository = customerReadRepository;
            }

            public async Task<CustomerListModel> Handle(GetListCustomerQuery request, CancellationToken cancellationToken)
            {
                var customers = await _customerReadRepository.GetListAsync(include: m => m.Include(p => p.User),
                    index: request.PageRequest.Page, size: request.PageRequest.PageSize);
                return _mapper.Map<CustomerListModel>(customers);
            }
        }
    }
}
