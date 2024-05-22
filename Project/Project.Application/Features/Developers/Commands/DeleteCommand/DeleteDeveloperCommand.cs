using Application.Features.Developers.Dtos;
using Application.Features.Developers.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Project.Application.Services.Repositories.Developers;

namespace Application.Features.Developers.Commands.DeleteCommand;

public class DeleteDeveloperCommand : IRequest<DeletedDeveloperDto>
{
    public string Id { get; set; }
    public bool isSoftDelete { get; set; }
    
    public class DeleteDeveloperCommandHandler:IRequestHandler<DeleteDeveloperCommand, DeletedDeveloperDto>
    {
        private readonly IDeveloperWriteRepository _developerWriteRepository;
        private readonly IMapper _mapper;
        private readonly DeveloperBusinessRules _developerBusinessRules;

        public DeleteDeveloperCommandHandler(IDeveloperWriteRepository developerWriteRepository, IMapper mapper, 
            DeveloperBusinessRules developerBusinessRules)
        {
            _mapper = mapper;
            _developerBusinessRules = developerBusinessRules;
            _developerWriteRepository = developerWriteRepository;
        }

        public async Task<DeletedDeveloperDto> Handle(DeleteDeveloperCommand request, CancellationToken cancellationToken)
        {
            var developer = await _developerBusinessRules.CheckIfDeveloperDoesNotExistsAndGetDeveloper(request.Id);

            Developer deletedDeveloper;
            if (request.isSoftDelete)
                deletedDeveloper = await _developerWriteRepository.SoftRemove(developer);
            else
                deletedDeveloper = await _developerWriteRepository.HardRemove(developer);
            
            return _mapper.Map<DeletedDeveloperDto>(deletedDeveloper);
        }
    }
}