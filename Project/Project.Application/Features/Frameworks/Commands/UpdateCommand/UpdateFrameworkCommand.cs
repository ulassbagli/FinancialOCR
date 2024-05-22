using Application.Features.Frameworks.Dtos.BaseDto;
using Application.Features.Frameworks.Rules;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Project.Application.Services.Repositories.Frameworks;

namespace Application.Features.Frameworks.Commands.UpdateCommand;

public class UpdateFrameworkCommand: IRequest<BaseFrameworkDto>
{
    public string Id { get; set; }
    public string? Name { get; set; }
    public string? Version { get; set; }
    public string? ProgrammingLanguageId { get; set; }

    public class UpdateFrameworkCommandHandler : IRequestHandler<UpdateFrameworkCommand, BaseFrameworkDto>
    {
        private readonly IFrameworkWriteRepository _frameworkWriteRepository;
        private readonly IFrameworkReadRepository _frameworkReadRepository;
        private readonly FrameworkBusinessRules _frameworkBusinessRules;
        private readonly IMapper _mapper;
        
        public UpdateFrameworkCommandHandler(IMapper mapper, IFrameworkWriteRepository frameworkWriteRepository, IFrameworkReadRepository frameworkReadRepository, FrameworkBusinessRules frameworkBusinessRules)
        {
            _frameworkWriteRepository = frameworkWriteRepository;
            _frameworkReadRepository = frameworkReadRepository;
            _frameworkBusinessRules = frameworkBusinessRules;
            _mapper = mapper;
        }
        
        public async Task<BaseFrameworkDto> Handle(UpdateFrameworkCommand request, CancellationToken cancellationToken)
        {
            var frameworkToUpdate = await _frameworkReadRepository.GetByIdAsync(request.Id);
            await _frameworkBusinessRules
                .CheckIfFrameworkDoesNotExists(frameworkToUpdate);

            _mapper.Map(request, frameworkToUpdate, typeof(UpdateFrameworkCommand), typeof(Framework));
            await _frameworkBusinessRules.CheckIfFrameworkNameIsAlreadyExists(frameworkToUpdate.Name);
            await _frameworkWriteRepository.Update(frameworkToUpdate);
            return _mapper.Map<BaseFrameworkDto>(frameworkToUpdate);
        }
    }
}