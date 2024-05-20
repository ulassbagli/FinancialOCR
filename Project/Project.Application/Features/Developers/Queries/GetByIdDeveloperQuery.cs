using Application.Features.Developers.Dtos.BaseDto;
using Application.Features.Developers.Rules;
using Application.Services.Repositories.Developers;
using AutoMapper;
using MediatR;

namespace Application.Features.Developers.Queries;

public class GetByIdDeveloperQuery : IRequest<BaseDeveloperDto>
{
    public string Id { get; set; }
    
    public class GetByIdDeveloperQueryHandler : IRequestHandler<GetByIdDeveloperQuery, BaseDeveloperDto>
    {
        private readonly DeveloperBusinessRules _developerBusinessRules;
        private readonly IMapper _mapper;
        
        public GetByIdDeveloperQueryHandler(IMapper mapper, DeveloperBusinessRules developerBusinessRules)
        {
            _developerBusinessRules = developerBusinessRules;
            _mapper = mapper;
        }
        
        public async Task<BaseDeveloperDto> Handle(GetByIdDeveloperQuery request, CancellationToken cancellationToken)
        {
            var developer = await _developerBusinessRules.CheckIfDeveloperDoesNotExistsAndGetDeveloper(request.Id);
            
            return _mapper.Map<BaseDeveloperDto>(developer);
        }
    }
}