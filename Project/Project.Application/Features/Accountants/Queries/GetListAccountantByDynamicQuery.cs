using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Core.Application.Requests;
using Core.Persistence.Dynamic;
using Core.Security.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Project.Application.Features.Accountants.Models;
using Project.Application.Services.Repositories.Accountants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Application.Features.Accountants.Queries;

public class GetListAccountantByDynamicQuery : IRequest<AccountantListModel>, ISecuredRequest
{
    public Dynamic Dynamic { get; set; }
    public PageRequest PageRequest { get; set; }

    public string[] Roles { get; } = { OperationClaimsEnum.Accountant.ToString() };

    public class GetListAccountantByDynamicQueryHandler : IRequestHandler<GetListAccountantByDynamicQuery, AccountantListModel>
    {
        private readonly IAccountantReadRepository _AccountantReadRepository;
        private readonly IMapper _mapper;

        public GetListAccountantByDynamicQueryHandler(IMapper mapper, IAccountantReadRepository AccountantReadRepository)
        {
            _AccountantReadRepository = AccountantReadRepository;
            _mapper = mapper;
        }

        public async Task<AccountantListModel> Handle(GetListAccountantByDynamicQuery request, CancellationToken cancellationToken)
        {
            var models = await _AccountantReadRepository.GetListByDynamicAsync(request.Dynamic, include:
                m => m.Include(c => c.User),
                index: request.PageRequest.Page,
                size: request.PageRequest.PageSize,
                cancellationToken: cancellationToken);
            return _mapper.Map<AccountantListModel>(models);
        }
    }
}
