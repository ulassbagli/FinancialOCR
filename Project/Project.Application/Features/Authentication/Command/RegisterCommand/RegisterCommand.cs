using System.Net;
using Application.Features.Authentication.Command.UserOperationClaimCommand;
using Application.Features.Authentication.Dtos;
using Application.Features.Authentication.Rules;
using Application.Services.Repositories.Users;
using AutoMapper;
using Core.CrossCuttingConcerns.Exceptions;
using Core.Security.Dtos;
using Core.Security.Entities;
using Core.Security.Enums;
using Core.Security.Hashing;
using MediatR;

namespace Application.Features.Authentication.Command.RegisterCommand;

public class RegisterCommand: UserForRegisterDto,IRequest<UserDto>
{
    public class RegisterCommandHandler : IRequestHandler<RegisterCommand, UserDto>
    {
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;
        private readonly IUserWriteRepository _userWriteRepository;
        private readonly AuthenticationBusinessRules _authenticationBusinessRules;
        
        public RegisterCommandHandler(IMapper mapper, IMediator mediator, IUserWriteRepository userWriteRepository, AuthenticationBusinessRules authenticationBusinessRules)
        {
            _mapper = mapper;
            _mediator = mediator;
            _userWriteRepository = userWriteRepository;
            _authenticationBusinessRules = authenticationBusinessRules;
        }

        public async Task<UserDto> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            var userToAdd = _mapper.Map<RegisterCommand, User>(request);
            await _authenticationBusinessRules.CheckIfUserAlreadyExists(userToAdd.Id.ToString());

            HashingHelper.CreatePasswordHash(request.Password, out byte[] passwordHash, out byte[] passwordSalt);
            userToAdd.PasswordHash = passwordHash;
            userToAdd.PasswordSalt = passwordSalt;

            await _userWriteRepository.AddAsync(userToAdd);
        
            await AddRoleToUserAsync(userToAdd);

            return _mapper.Map<User, UserDto>(userToAdd);
        }

        private async Task AddRoleToUserAsync(User userToAdd)
        {
            await _mediator.Send(new CreateUserOperationClaimCommand
            {
                User = userToAdd,
                OperationClaims = new HashSet<OperationClaim>() { new() { Name = OperationClaimsEnum.User.ToString()} }
            });
        }
    }
}