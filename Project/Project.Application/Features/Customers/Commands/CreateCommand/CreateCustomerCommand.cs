using Application.Features.Authentication.Command.UserOperationClaimCommand;
using Application.Services.Repositories.Users;
using AutoMapper;
using Core.Security.Entities;
using Core.Security.Enums;
using Domain.Entities;
using MediatR;
using Project.Application.Features.Customers.Dtos.BaseDto;
using Project.Application.Features.Customers.Rules;
using Project.Application.Services.Repositories.Customers;
using Project.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Application.Features.Customers.Commands.CreateCommand
{
    public class CreateCustomerCommand : IRequest<BaseCustomerDto>
    {
        public string UserId { get; set; }

        public class CreateCustomerCommandHandler : IRequestHandler<CreateCustomerCommand, BaseCustomerDto>
        {
            private readonly IMapper _mapper;
            private readonly IMediator _mediator;
            private readonly ICustomerWriteRepository _customerWriteRepository;
            private readonly IUserReadRepository _userReadRepository;
            private readonly CustomerBusinessRules _customerBusinessRules;

            public CreateCustomerCommandHandler(ICustomerWriteRepository customerWriteRepository, IMapper mapper,
                CustomerBusinessRules customerBusinessRules, IMediator mediator, IUserReadRepository userReadRepository)
            {
                _mapper = mapper;
                _customerBusinessRules = customerBusinessRules;
                _mediator = mediator;
                _userReadRepository = userReadRepository;
                _customerWriteRepository = customerWriteRepository;
            }

            public async Task<BaseCustomerDto> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
            {
                User user = await _customerBusinessRules.CheckIfUserDoesNotExistsAndGetUser(request.UserId);
                await _customerBusinessRules.CheckIfCustomerAlreadyExists(request.UserId);

                var mappedCustomer = _mapper.Map<Customer>(request);
                var createdCustomer = await _customerWriteRepository.AddAsync(mappedCustomer);
                await AddRoleToUserAsync(user);
                return _mapper.Map<BaseCustomerDto>(createdCustomer);
            }

            private async Task AddRoleToUserAsync(User userToAdd)
            {
                await _mediator.Send(new CreateUserOperationClaimCommand
                {
                    User = userToAdd,
                    OperationClaims = new HashSet<OperationClaim>() { new() { Name = OperationClaimsEnum.Customer.ToString() } }
                });
            }
        }
    }
}
