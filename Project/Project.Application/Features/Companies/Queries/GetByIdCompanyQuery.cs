using AutoMapper;
using MediatR;
using Project.Application.Features.Companies.Dtos.BaseDto;
using Project.Application.Features.Companies.Rules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Application.Features.Companies.Queries
{
    public class GetByIdCompanyQuery : IRequest<BaseCompanyDto>
    {
        public string Id { get; set; }

        public class GetByIdCompanyQueryHandler : IRequestHandler<GetByIdCompanyQuery, BaseCompanyDto>
        {
            private readonly CompanyBusinessRules _companyBusinessRules;
            private readonly IMapper _mapper;

            public GetByIdCompanyQueryHandler(IMapper mapper, CompanyBusinessRules companyBusinessRules)
            {
                _companyBusinessRules = companyBusinessRules;
                _mapper = mapper;
            }

            public async Task<BaseCompanyDto> Handle(GetByIdCompanyQuery request, CancellationToken cancellationToken)
            {
                var company = await _companyBusinessRules.CheckIfCompanyDoesNotExistsAndGetCompany(request.Id);

                return _mapper.Map<BaseCompanyDto>(company);
            }
        }
    }
}
