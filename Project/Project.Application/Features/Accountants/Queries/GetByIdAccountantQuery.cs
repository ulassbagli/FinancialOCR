using AutoMapper;
using MediatR;
using Project.Application.Features.Accountants.Dtos.BaseDto;
using Project.Application.Features.Accountants.Rules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Application.Features.Accountants.Queries
{
    public class GetByIdAccountantQuery : IRequest<BaseAccountantDto>
    {
        public string Id { get; set; }

        public class GetByIdAccountantQueryHandler : IRequestHandler<GetByIdAccountantQuery, BaseAccountantDto>
        {
            private readonly AccountantBusinessRules _accountantBusinessRules;
            private readonly IMapper _mapper;

            public GetByIdAccountantQueryHandler(IMapper mapper, AccountantBusinessRules accountantBusinessRules)
            {
                _accountantBusinessRules = accountantBusinessRules;
                _mapper = mapper;
            }

            public async Task<BaseAccountantDto> Handle(GetByIdAccountantQuery request, CancellationToken cancellationToken)
            {
                var accountant = await _accountantBusinessRules.CheckIfAccountantDoesNotExistsAndGetAccountant(request.Id);

                return _mapper.Map<BaseAccountantDto>(accountant);
            }
        }
    }
}
