using Application.Features.Frameworks.Dtos.BaseDto;
using Application.Features.Frameworks.Rules;
using AutoMapper;
using MediatR;
using Project.Application.Services.Repositories.Frameworks;

namespace Application.Features.Frameworks.Queries;

public class GetByIdFrameworkQuery: IRequest<BaseFrameworkDto>
{
    public string Id { get; set; }
    
    public class GetByIdFrameworkQueryHandler : IRequestHandler<GetByIdFrameworkQuery, BaseFrameworkDto>
    {
        private readonly IFrameworkReadRepository _frameworkReadRepository;
        private readonly FrameworkBusinessRules _frameworkBusinessRules;
        private readonly IMapper _mapper;
        
        public GetByIdFrameworkQueryHandler(IFrameworkReadRepository frameworkReadRepository, IMapper mapper, FrameworkBusinessRules frameworkBusinessRules)
        {
            _frameworkReadRepository = frameworkReadRepository;
            _frameworkBusinessRules = frameworkBusinessRules;
            _mapper = mapper;
        }
        
        public async Task<BaseFrameworkDto> Handle(GetByIdFrameworkQuery request, CancellationToken cancellationToken)
        {
            var framework = await _frameworkReadRepository.GetByIdAsync(request.Id);
            await _frameworkBusinessRules.CheckIfFrameworkDoesNotExists(framework);
            
            return _mapper.Map<BaseFrameworkDto>(framework);
        }
    }
}