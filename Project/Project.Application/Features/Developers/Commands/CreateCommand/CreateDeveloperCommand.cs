using Application.Features.Authentication.Command.UserOperationClaimCommand;
using Application.Features.Developers.Dtos.BaseDto;
using Application.Features.Developers.Rules;
using Application.Services.Repositories.Developers;
using Application.Services.Repositories.Users;
using AutoMapper;
using Core.Security.Entities;
using Core.Security.Enums;
using Domain.Entities;
using MediatR;

namespace Application.Features.Developers.Commands.CreateCommand;

public class CreateDeveloperCommand : IRequest<BaseDeveloperDto>
{
    public string UserId { get; set; }

    public class CreateDeveloperCommandHandler:IRequestHandler<CreateDeveloperCommand, BaseDeveloperDto>
    {
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;
        private readonly IDeveloperWriteRepository _developerWriteRepository;
        private readonly IUserReadRepository _userReadRepository;
        private readonly DeveloperBusinessRules _developerBusinessRules;

        public CreateDeveloperCommandHandler(IDeveloperWriteRepository developerWriteRepository, IMapper mapper, 
            DeveloperBusinessRules developerBusinessRules, IMediator mediator, IUserReadRepository userReadRepository)
        {
            _mapper = mapper;
            _developerBusinessRules = developerBusinessRules;
            _mediator = mediator;
            _userReadRepository = userReadRepository;
            _developerWriteRepository = developerWriteRepository;
        }

        public async Task<BaseDeveloperDto> Handle(CreateDeveloperCommand request, CancellationToken cancellationToken)
        {
            User user = await _developerBusinessRules.CheckIfUserDoesNotExistsAndGetUser(request.UserId);
            await _developerBusinessRules.CheckIfDeveloperAlreadyExists(request.UserId);
            
            var mappedDeveloper = _mapper.Map<Developer>(request);
            var createdDeveloper = await _developerWriteRepository.AddAsync(mappedDeveloper);
            await AddRoleToUserAsync(user);
            return _mapper.Map<BaseDeveloperDto>(createdDeveloper);
        }
        
        private async Task AddRoleToUserAsync(User userToAdd)
        {
            await _mediator.Send(new CreateUserOperationClaimCommand
            {
                User = userToAdd,
                OperationClaims = new HashSet<OperationClaim>() { new() { Name = OperationClaimsEnum.Developer.ToString()} }
            });
        }
    }
}