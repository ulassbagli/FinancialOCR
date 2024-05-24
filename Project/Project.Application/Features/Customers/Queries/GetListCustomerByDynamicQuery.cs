using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Core.Application.Requests;
using Core.Persistence.Dynamic;
using Core.Security.Enums;
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
    public class GetListCustomerByDynamicQuery : IRequest<CustomerListModel>, ISecuredRequest
    {
        public Dynamic Dynamic { get; set; }
        public PageRequest PageRequest { get; set; }

        public string[] Roles { get; } = { OperationClaimsEnum.Customer.ToString() };

        public class GetListCustomerByDynamicQueryHandler : IRequestHandler<GetListCustomerByDynamicQuery, CustomerListModel>
        {
            private readonly ICustomerReadRepository _customerReadRepository;
            private readonly IMapper _mapper;

            public GetListCustomerByDynamicQueryHandler(IMapper mapper, ICustomerReadRepository customerReadRepository)
            {
                _customerReadRepository = customerReadRepository;
                _mapper = mapper;
            }

            public async Task<CustomerListModel> Handle(GetListCustomerByDynamicQuery request, CancellationToken cancellationToken)
            {
                var models = await _customerReadRepository.GetListByDynamicAsync(request.Dynamic, include:
                    m => m.Include(c => c.User),
                    index: request.PageRequest.Page,
                    size: request.PageRequest.PageSize,
                    cancellationToken: cancellationToken);
                return _mapper.Map<CustomerListModel>(models);
            }
        }
    }
}
