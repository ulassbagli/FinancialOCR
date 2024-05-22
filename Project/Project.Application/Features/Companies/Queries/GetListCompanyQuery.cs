using AutoMapper;
using Core.Application.Requests;
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
    public class GetListCompanyQuery : IRequest<CompanyListModel>
    {
        public PageRequest PageRequest { get; set; }

        public class GetListCompanyQueryHandler : IRequestHandler<GetListCompanyQuery, CompanyListModel>
        {
            private readonly IMapper _mapper;
            private readonly ICompanyReadRepository _companyReadRepository;

            public GetListCompanyQueryHandler(ICompanyReadRepository companyReadRepository, IMapper mapper)
            {
                _mapper = mapper;
                _companyReadRepository = companyReadRepository;
            }

            public async Task<CompanyListModel> Handle(GetListCompanyQuery request, CancellationToken cancellationToken)
            {
                var companys = await _companyReadRepository.GetListAsync(include: m => m.Include(p => p.User),
                    index: request.PageRequest.Page, size: request.PageRequest.PageSize);
                return _mapper.Map<CompanyListModel>(companys);
            }
        }
    }
}
