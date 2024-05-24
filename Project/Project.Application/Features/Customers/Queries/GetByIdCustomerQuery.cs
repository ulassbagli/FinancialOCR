using AutoMapper;
using MediatR;
using Project.Application.Features.Customers.Dtos.BaseDto;
using Project.Application.Features.Customers.Rules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Application.Features.Customers.Queries
{
    public class GetByIdCustomerQuery : IRequest<BaseCustomerDto>
    {
        public string Id { get; set; }

        public class GetByIdCustomerQueryHandler : IRequestHandler<GetByIdCustomerQuery, BaseCustomerDto>
        {
            private readonly CustomerBusinessRules _customerBusinessRules;
            private readonly IMapper _mapper;

            public GetByIdCustomerQueryHandler(IMapper mapper, CustomerBusinessRules customerBusinessRules)
            {
                _customerBusinessRules = customerBusinessRules;
                _mapper = mapper;
            }

            public async Task<BaseCustomerDto> Handle(GetByIdCustomerQuery request, CancellationToken cancellationToken)
            {
                var customer = await _customerBusinessRules.CheckIfCustomerDoesNotExistsAndGetCustomer(request.Id);

                return _mapper.Map<BaseCustomerDto>(customer);
            }
        }
    }
}
