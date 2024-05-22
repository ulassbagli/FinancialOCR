using Application.Features.Frameworks.Models;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Core.Application.Requests;
using Core.Persistence.Dynamic;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Project.Application.Services.Repositories.Frameworks;

namespace Application.Features.Frameworks.Queries;

public class GetListFrameworkByDynamicQuery : IRequest<FrameworkListModel>, ISecuredRequest
{
    public Dynamic Dynamic { get; set; }
    public PageRequest PageRequest { get; set; }
    
    public string[] Roles { get; } = { "User" };
    
    public class GetListFrameworkByDynamicQueryHandler : IRequestHandler<GetListFrameworkByDynamicQuery,FrameworkListModel>
    {
        private readonly IFrameworkReadRepository _frameworkReadRepository;
        private readonly IMapper _mapper;

        public GetListFrameworkByDynamicQueryHandler(IMapper mapper, IFrameworkReadRepository frameworkReadRepository)
        {
            _frameworkReadRepository = frameworkReadRepository;
            _mapper = mapper;
        }

        public async Task<FrameworkListModel> Handle(GetListFrameworkByDynamicQuery request, CancellationToken cancellationToken)
        {
            var models = await _frameworkReadRepository.GetListByDynamicAsync(request.Dynamic,include:
                m => m.Include(c => c.ProgrammingLanguage),
                index: request.PageRequest.Page,
                size: request.PageRequest.PageSize, 
                cancellationToken: cancellationToken);
            return _mapper.Map<FrameworkListModel>(models);
        }
    }
}