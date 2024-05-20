using Application.Features.ProgrammingLanguages.Dtos.BaseDto;
using Application.Features.ProgrammingLanguages.Rules;
using Application.Services.Repositories;
using Application.Services.Repositories.ProgrammingLanguages;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.ProgrammingLanguages.Queries;

public class GetByIdProgrammingLanguageQuery : IRequest<BaseProgrammingLanguageDto>
{
    public string Id { get; set; }
    
    public class GetByIdProgrammingLanguageQueryHandler : IRequestHandler<GetByIdProgrammingLanguageQuery, BaseProgrammingLanguageDto>
    {
        private readonly IProgrammingLanguageReadRepository _programmingLanguageReadRepository;
        private readonly ProgrammingLanguageBusinessRules _programmingLanguageBusinessRules;
        private readonly IMapper _mapper;
        
        public GetByIdProgrammingLanguageQueryHandler(IProgrammingLanguageReadRepository programmingLanguageReadRepository, IMapper mapper, ProgrammingLanguageBusinessRules programmingLanguageBusinessRules)
        {
            _programmingLanguageReadRepository = programmingLanguageReadRepository;
            _programmingLanguageBusinessRules = programmingLanguageBusinessRules;
            _mapper = mapper;
        }
        
        public async Task<BaseProgrammingLanguageDto> Handle(GetByIdProgrammingLanguageQuery request, CancellationToken cancellationToken)
        {
            var programmingLanguage = await _programmingLanguageReadRepository.GetByIdAsync(request.Id);
            await _programmingLanguageBusinessRules.CheckIfProgrammingLanguageDoesNotExists(programmingLanguage);
            
            return _mapper.Map<BaseProgrammingLanguageDto>(programmingLanguage);
        }
    }
}