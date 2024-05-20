using Application.Features.Developers.Dtos;
using Application.Features.Developers.Dtos.BaseDto;
using Application.Features.Developers.Rules;
using Application.Services.Repositories;
using Application.Services.Repositories.Developers;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.Developers.Commands.UpdateCommand;

public class UpdateDeveloperCommand : IRequest<BaseDeveloperDto>
{
    public string Id { get; set; }
    public string UserId { get; set; }

    public class UpdateDeveloperCommandHandler : IRequestHandler<UpdateDeveloperCommand, BaseDeveloperDto>
    {
        private readonly IDeveloperWriteRepository _developerWriteRepository;
        private readonly DeveloperBusinessRules _developerBusinessRules;
        private readonly IMapper _mapper;
        
        public UpdateDeveloperCommandHandler(IMapper mapper, IDeveloperWriteRepository developerWriteRepository, DeveloperBusinessRules developerBusinessRules)
        {
            _developerWriteRepository = developerWriteRepository;
            _developerBusinessRules = developerBusinessRules;
            _mapper = mapper;
        }
        
        public async Task<BaseDeveloperDto> Handle(UpdateDeveloperCommand request, CancellationToken cancellationToken)
        {
            var developerToUpdate = await _developerBusinessRules.CheckIfDeveloperDoesNotExistsAndGetDeveloper(request.Id);
            await _developerBusinessRules.CheckIfUserDoesNotExistsAndGetUser(request.UserId);
            
            _mapper.Map(request, developerToUpdate, typeof(UpdateDeveloperCommand), typeof(Developer));
            await _developerWriteRepository.Update(developerToUpdate);
            return _mapper.Map<BaseDeveloperDto>(developerToUpdate);
        }
    }
}