using Application.Features.Authentication.Command.UserOperationClaimCommand;
using Application.Services.Repositories.Users;
using AutoMapper;
using Core.Security.Entities;
using Core.Security.Enums;
using Domain.Entities;
using MediatR;
using Project.Application.Features.Authentication.Command.CustomerOperationClaimCommand;
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
        public string CustomerId { get; set; }

        public class CreateCustomerCommandHandler : IRequestHandler<CreateCustomerCommand, BaseCustomerDto>
        {
            private readonly IMapper _mapper;
            private readonly IMediator _mediator;
            private readonly ICustomerWriteRepository _customerWriteRepository;
            private readonly IUserReadRepository _userReadRepository;
            private readonly CustomerBusinessRules _customerBusinessRules;
            private readonly ICustomerReadRepository _customerReadRepository;

            public CreateCustomerCommandHandler(ICustomerWriteRepository customerWriteRepository, IMapper mapper,
                CustomerBusinessRules customerBusinessRules, IMediator mediator, IUserReadRepository userReadRepository, ICustomerReadRepository customerReadRepository)
            {
                _mapper = mapper;
                _customerBusinessRules = customerBusinessRules;
                _mediator = mediator;
                _userReadRepository = userReadRepository;
                _customerWriteRepository = customerWriteRepository;
                _customerReadRepository = customerReadRepository;
            }

            public async Task<BaseCustomerDto> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
            {
                Customer customer = await _customerBusinessRules.CheckIfCustomerDoesNotExistsAndGetCustomer(request.CustomerId);
                await _customerBusinessRules.CheckIfCustomerAlreadyExists(request.CustomerId);

                var mappedCustomer = _mapper.Map<Customer>(request);
                var createdCustomer = await _customerWriteRepository.AddAsync(mappedCustomer);
                await AddRoleToCustomerAsync(customer);
                return _mapper.Map<BaseCustomerDto>(createdCustomer);
            }

            private async Task AddRoleToCustomerAsync(Customer customerToAdd)
            {
                await _mediator.Send(new CreateCustomerOperationClaimCommand
                {
                    Customer = customerToAdd,
                    OperationClaims = new HashSet<OperationClaim>() { new() { Name = OperationClaimsEnum.Customer.ToString() } }
                });
            }
        }
    }
}
