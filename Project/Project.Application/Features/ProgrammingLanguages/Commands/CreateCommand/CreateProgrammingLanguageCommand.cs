using Application.Features.ProgrammingLanguages.Dtos.BaseDto;
using Application.Features.ProgrammingLanguages.Rules;
using Application.Services.Repositories;
using Application.Services.Repositories.ProgrammingLanguages;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.ProgrammingLanguages.Commands.CreateCommand;

public class CreateProgrammingLanguageCommand : IRequest<BaseProgrammingLanguageDto>
{
    public string Name { get; set; }
    
    public class CreateProgrammingLanguageCommandHandler:IRequestHandler<CreateProgrammingLanguageCommand, BaseProgrammingLanguageDto>
    {
        private readonly IProgrammingLanguageWriteRepository _programmingLanguageWriteRepository;
        private readonly IMapper _mapper;
        private readonly ProgrammingLanguageBusinessRules _programmingLanguageBusinessRules;

        public CreateProgrammingLanguageCommandHandler(IProgrammingLanguageWriteRepository programmingLanguageWriteRepository, IMapper mapper, ProgrammingLanguageBusinessRules programmingLanguageBusinessRules)
        {
            _mapper = mapper;
            _programmingLanguageBusinessRules = programmingLanguageBusinessRules;
            _programmingLanguageWriteRepository = programmingLanguageWriteRepository;
        }

        public async Task<BaseProgrammingLanguageDto> Handle(CreateProgrammingLanguageCommand request, CancellationToken cancellationToken)
        {
            await _programmingLanguageBusinessRules.CheckIfProgrammingLanguageNameIsAlreadyExists(request.Name);
            
            var mappedProgrammingLanguage = _mapper.Map<ProgrammingLanguage>(request);
            var createdProgramminLanguage = await _programmingLanguageWriteRepository.AddAsync(mappedProgrammingLanguage);
            var createdProgrammingLanguageDTO = _mapper.Map<BaseProgrammingLanguageDto>(createdProgramminLanguage);
            return createdProgrammingLanguageDTO;
        }
    }
}