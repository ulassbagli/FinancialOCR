using Application.Features.Authentication.Command.UserOperationClaimCommand;
using Application.Features.Developers.Commands.CreateCommand;
using Application.Features.Developers.Dtos.BaseDto;
using Application.Features.Developers.Rules;
using Application.Services.Repositories.Developers;
using Application.Services.Repositories.Users;
using AutoMapper;
using Core.Security.Entities;
using Core.Security.Enums;
using Domain.Entities;
using MediatR;
using Project.Application.Features.Accountants.Dtos.BaseDto;
using Project.Application.Features.Accountants.Rules;
using Project.Application.Services.Repositories.Accountants;
using Project.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Application.Features.Accountants.Commands.CreateCommand
{
    public class CreateAccountantCommand : IRequest<BaseAccountantDto>
    {
        public string AccountantId { get; set; }

        public class CreateAccountantCommandHandler : IRequestHandler<CreateAccountantCommand, BaseAccountantDto>
        {
            private readonly IMapper _mapper;
            private readonly IMediator _mediator;
            private readonly IAccountantWriteRepository _accountantWriteRepository;
            private readonly IUserReadRepository _userReadRepository;
            private readonly AccountantBusinessRules _accountantBusinessRules;

            public CreateAccountantCommandHandler(IAccountantWriteRepository accountantWriteRepository, IMapper mapper,
                AccountantBusinessRules accountantBusinessRules, IMediator mediator, IUserReadRepository userReadRepository)
            {
                _mapper = mapper;
                _accountantBusinessRules = accountantBusinessRules;
                _mediator = mediator;
                _userReadRepository = userReadRepository;
                _accountantWriteRepository = accountantWriteRepository;
            }

            public async Task<BaseAccountantDto> Handle(CreateAccountantCommand request, CancellationToken cancellationToken)
            {
                User user = await _accountantBusinessRules.CheckIfAccountantDoesNotExistsAndGetAccountant(request.AccountantId);
                await _accountantBusinessRules.CheckIfAccountantAlreadyExists(request.AccountantId);

                var mappedAccountant = _mapper.Map<Accountant>(request);
                var createdAccountant = await _accountantWriteRepository.AddAsync(mappedAccountant);
                await AddRoleToUserAsync(user);
                return _mapper.Map<BaseAccountantDto>(createdAccountant);
            }

            private async Task AddRoleToUserAsync(User userToAdd)
            {
                await _mediator.Send(new CreateUserOperationClaimCommand
                {
                    User = userToAdd,
                    OperationClaims = new HashSet<OperationClaim>() { new() { Name = OperationClaimsEnum.Accountant.ToString() } }
                });
            }
        }
    }
}
