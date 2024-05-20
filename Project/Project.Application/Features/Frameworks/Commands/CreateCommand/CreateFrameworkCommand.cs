using Application.Features.Frameworks.Dtos.BaseDto;
using Application.Features.Frameworks.Rules;
using Application.Services.Repositories.Frameworks;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.Frameworks.Commands.CreateCommand;

public class CreateFrameworkCommand : IRequest<BaseFrameworkDto>
{
    public string Name { get; set; }
    public string? Version { get; set; }
    public string ProgrammingLanguageId { get; set; }
    
    public class CreateFrameworkCommandHandler:IRequestHandler<CreateFrameworkCommand, BaseFrameworkDto>
    {
        private readonly IFrameworkWriteRepository _frameworkWriteRepository;
        private readonly FrameworkBusinessRules _frameworkBusinessRules;
        private readonly IMapper _mapper;

        public CreateFrameworkCommandHandler(IFrameworkWriteRepository frameworkWriteRepository, FrameworkBusinessRules frameworkBusinessRules, IMapper mapper)
        {
            _frameworkWriteRepository = frameworkWriteRepository;
            _frameworkBusinessRules = frameworkBusinessRules;
            _mapper = mapper;
        }

        public async Task<BaseFrameworkDto> Handle(CreateFrameworkCommand request, CancellationToken cancellationToken)
        {
            await _frameworkBusinessRules.CheckIfFrameworkNameIsAlreadyExists(request.Name);
            
            var mappedframework = _mapper.Map<Framework>(request);
            var createdFramework = await _frameworkWriteRepository.AddAsync(mappedframework);
            return _mapper.Map<BaseFrameworkDto>(createdFramework);
        }
    }
}