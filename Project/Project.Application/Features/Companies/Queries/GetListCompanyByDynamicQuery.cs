using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Core.Application.Requests;
using Core.Persistence.Dynamic;
using Core.Security.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Project.Application.Features.Companies.Models;
using Project.Application.Services.Repositories.Companies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Application.Features.Companies.Queries
{
    public class GetListCompanyByDynamicQuery : IRequest<CompanyListModel>, ISecuredRequest
    {
        public Dynamic Dynamic { get; set; }
        public PageRequest PageRequest { get; set; }

        public string[] Roles { get; } = { OperationClaimsEnum.Company.ToString() };

        public class GetListCompanyByDynamicQueryHandler : IRequestHandler<GetListCompanyByDynamicQuery, CompanyListModel>
        {
            private readonly ICompanyReadRepository _companyReadRepository;
            private readonly IMapper _mapper;

            public GetListCompanyByDynamicQueryHandler(IMapper mapper, ICompanyReadRepository companyReadRepository)
            {
                _companyReadRepository = companyReadRepository;
                _mapper = mapper;
            }

            public async Task<CompanyListModel> Handle(GetListCompanyByDynamicQuery request, CancellationToken cancellationToken)
            {
                var models = await _companyReadRepository.GetListByDynamicAsync(request.Dynamic, include:
                    m => m.Include(c => c.User),
                    index: request.PageRequest.Page,
                    size: request.PageRequest.PageSize,
                    cancellationToken: cancellationToken);
                return _mapper.Map<CompanyListModel>(models);
            }
        }
    }
}
