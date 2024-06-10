using AutoMapper;
using Core.Application.Requests;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Project.Application.Features.Accountants.Models;
using Project.Application.Services.Repositories.Accountants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Application.Features.Accountants.Queries
{
    public class GetListAccountantQuery : IRequest<AccountantListModel>
    {
        public PageRequest PageRequest { get; set; }

        public class GetListAccountantQueryHandler : IRequestHandler<GetListAccountantQuery, AccountantListModel>
        {
            private readonly IMapper _mapper;
            private readonly IAccountantReadRepository _AccountantReadRepository;

            public GetListAccountantQueryHandler(IAccountantReadRepository AccountantReadRepository, IMapper mapper)
            {
                _mapper = mapper;
                _AccountantReadRepository = AccountantReadRepository;
            }

            public async Task<AccountantListModel> Handle(GetListAccountantQuery request, CancellationToken cancellationToken)
            {
                var Accountants = await _AccountantReadRepository.GetListAsync(include: m => m.Include(p => p.User),
                    index: request.PageRequest.Page, size: request.PageRequest.PageSize);
                return _mapper.Map<AccountantListModel>(Accountants);
            }
        }
    }
}
