using Application.Features.ProgrammingLanguages.Models;
using Application.Services.Repositories.ProgrammingLanguages;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Core.Application.Requests;
using Core.Persistence.Dynamic;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.ProgrammingLanguages.Queries;

public class GetListProgrammingLanguageByDynamicQuery : IRequest<ProgrammingLanguageListModel>, ISecuredRequest
{
    public Dynamic Dynamic { get; set; }
    public PageRequest PageRequest { get; set; }
    
    public string[] Roles { get; } = { "User" };
    
    public class GetListProgrammingLanguageByDynamicQueryHandler : IRequestHandler<GetListProgrammingLanguageByDynamicQuery,ProgrammingLanguageListModel>
    {
        private readonly IProgrammingLanguageReadRepository _programmingLanguageReadRepository;
        private readonly IMapper _mapper;

        public GetListProgrammingLanguageByDynamicQueryHandler(IMapper mapper, IProgrammingLanguageReadRepository programmingLanguageReadRepository)
        {
            _programmingLanguageReadRepository = programmingLanguageReadRepository;
            _mapper = mapper;
        }

        public async Task<ProgrammingLanguageListModel> Handle(GetListProgrammingLanguageByDynamicQuery request, CancellationToken cancellationToken)
        {
            var models = await _programmingLanguageReadRepository.GetListByDynamicAsync(request.Dynamic,include:
                m => m.Include(c => c.Frameworks),
                index: request.PageRequest.Page,
                size: request.PageRequest.PageSize, 
                cancellationToken: cancellationToken);
            return _mapper.Map<ProgrammingLanguageListModel>(models);
        }
    }
}