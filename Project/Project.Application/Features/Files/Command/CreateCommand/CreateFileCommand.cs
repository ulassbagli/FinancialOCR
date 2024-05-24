using Application.Features.Authentication.Command.UserOperationClaimCommand;
using Application.Services.Repositories.Users;
using AutoMapper;
using Core.Security.Entities;
using Core.Security.Enums;
using Domain.Entities;
using MediatR;
using Project.Application.Features.Files.Dtos.BaseDto;
using Project.Application.Features.Files.Rules;
using Project.Application.Services.Repositories.Files;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using File = Project.Domain.Entities.File;

namespace Project.Application.Features.Files.Command.CreateCommand
{
    public class CreateFileCommand : IRequest<BaseFileDto>
    {
        public string UserId { get; set; }

        public class CreateFileCommandHandler : IRequestHandler<CreateFileCommand, BaseFileDto>
        {
            private readonly IMapper _mapper;
            private readonly IMediator _mediator;
            private readonly IFileWriteRepository _fileWriteRepository;
            private readonly IUserReadRepository _userReadRepository;
            private readonly FileBusinessRules _fileBusinessRules;

            public CreateFileCommandHandler(IFileWriteRepository fileWriteRepository, IMapper mapper,
                FileBusinessRules fileBusinessRules, IMediator mediator, IUserReadRepository userReadRepository)
            {
                _mapper = mapper;
                _fileBusinessRules = fileBusinessRules;
                _mediator = mediator;
                _userReadRepository = userReadRepository;
                _fileWriteRepository = fileWriteRepository;
            }

            public async Task<BaseFileDto> Handle(CreateFileCommand request, CancellationToken cancellationToken)
            {
                User user = await _fileBusinessRules.CheckIfUserDoesNotExistsAndGetUser(request.UserId);
                await _fileBusinessRules.CheckIfFileAlreadyExists(request.UserId);

                var mappedFile = _mapper.Map<File>(request);
                var createdFile = await _fileWriteRepository.AddAsync(mappedFile);
                await AddRoleToUserAsync(user);
                return _mapper.Map<BaseFileDto>(createdFile);
            }

            private async Task AddRoleToUserAsync(User userToAdd)
            {
                await _mediator.Send(new CreateUserOperationClaimCommand
                {
                    User = userToAdd,
                    OperationClaims = new HashSet<OperationClaim>() { new() { Name = OperationClaimsEnum.File.ToString() } }
                });
            }
        }
    }
}
