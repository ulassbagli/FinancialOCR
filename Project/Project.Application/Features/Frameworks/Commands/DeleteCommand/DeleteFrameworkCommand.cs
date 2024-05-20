using Application.Features.Frameworks.Dtos;
using Application.Features.Frameworks.Rules;
using Application.Services.Repositories.Frameworks;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.Frameworks.Commands.DeleteCommand;

public class DeleteFrameworkCommand: IRequest<DeletedFrameworkDto>
{
    public string Id { get; set; }
    public bool isSoftDelete { get; set; }
    
    public class DeleteFrameworkCommandHandler:IRequestHandler<DeleteFrameworkCommand, DeletedFrameworkDto>
    {
        private readonly IFrameworkWriteRepository _frameworkWriteRepository;
        private readonly IFrameworkReadRepository _frameworkReadRepository;
        private readonly IMapper _mapper;
        private readonly FrameworkBusinessRules _frameworkBusinessRules;

        public DeleteFrameworkCommandHandler(IMapper mapper, IFrameworkWriteRepository frameworkWriteRepository, IFrameworkReadRepository frameworkReadRepository, FrameworkBusinessRules frameworkBusinessRules)
        {
            _mapper = mapper;
            _frameworkWriteRepository = frameworkWriteRepository;
            _frameworkReadRepository = frameworkReadRepository;
            _frameworkBusinessRules = frameworkBusinessRules;
        }

        public async Task<DeletedFrameworkDto> Handle(DeleteFrameworkCommand request, CancellationToken cancellationToken)
        {
            var Framework = await _frameworkReadRepository.GetByIdAsync(request.Id);
            await _frameworkBusinessRules.CheckIfFrameworkDoesNotExists(Framework);

            Framework deletedFramework;
            if (request.isSoftDelete)
                deletedFramework = await _frameworkWriteRepository.SoftRemove(Framework);
            else
                deletedFramework = await _frameworkWriteRepository.HardRemove(Framework);
            
            return _mapper.Map<DeletedFrameworkDto>(deletedFramework);
        }
    }
}