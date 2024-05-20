using Application.Features.ProgrammingLanguages.Dtos;
using Application.Features.ProgrammingLanguages.Rules;
using Application.Services.Repositories;
using Application.Services.Repositories.ProgrammingLanguages;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.ProgrammingLanguages.Commands.DeleteCommand;

public class DeleteProgrammingLanguageCommand : IRequest<DeletedProgrammingLanguageDto>
{
    public string Id { get; set; }
    public bool isSoftDelete { get; set; }
    
    public class DeleteProgrammingLanguageCommandHandler:IRequestHandler<DeleteProgrammingLanguageCommand, DeletedProgrammingLanguageDto>
    {
        private readonly IProgrammingLanguageWriteRepository _programmingLanguageWriteRepository;
        private readonly IProgrammingLanguageReadRepository _programmingLanguageReadRepository;
        private readonly IMapper _mapper;
        private readonly ProgrammingLanguageBusinessRules _programmingLanguageBusinessRules;

        public DeleteProgrammingLanguageCommandHandler(IProgrammingLanguageWriteRepository programmingLanguageWriteRepository, IMapper mapper, ProgrammingLanguageBusinessRules programmingLanguageBusinessRules, IProgrammingLanguageReadRepository programmingLanguageReadRepository)
        {
            _mapper = mapper;
            _programmingLanguageBusinessRules = programmingLanguageBusinessRules;
            _programmingLanguageReadRepository = programmingLanguageReadRepository;
            _programmingLanguageWriteRepository = programmingLanguageWriteRepository;
        }

        public async Task<DeletedProgrammingLanguageDto> Handle(DeleteProgrammingLanguageCommand request, CancellationToken cancellationToken)
        {
            var programmingLanguage = await _programmingLanguageReadRepository.GetByIdAsync(request.Id);
            await _programmingLanguageBusinessRules.CheckIfProgrammingLanguageDoesNotExists(programmingLanguage);

            ProgrammingLanguage deletedProgrammingLanguage;
            if (request.isSoftDelete)
                deletedProgrammingLanguage = await _programmingLanguageWriteRepository.SoftRemove(programmingLanguage);
            else
                deletedProgrammingLanguage = await _programmingLanguageWriteRepository.HardRemove(programmingLanguage);
            
            return _mapper.Map<DeletedProgrammingLanguageDto>(deletedProgrammingLanguage);
        }
    }
}