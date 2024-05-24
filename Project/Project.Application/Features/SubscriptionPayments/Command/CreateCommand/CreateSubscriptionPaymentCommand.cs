using Application.Features.Authentication.Command.UserOperationClaimCommand;
using Application.Services.Repositories.Users;
using AutoMapper;
using Core.Security.Entities;
using Core.Security.Enums;
using Domain.Entities;
using MediatR;
using Project.Application.Features.SubscriptionPayments.Dtos.BaseDto;
using Project.Application.Features.SubscriptionPayments.Rules;
using Project.Application.Services.Repositories.SubscriptionPayments;
using Project.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Application.Features.SubscriptionPayments.Command.CreateCommand
{
    public class CreateSubscriptionPaymentCommand : IRequest<BaseSubscriptionPaymentDto>
    {
        public string UserId { get; set; }

        public class CreateSubscriptionPaymentCommandHandler : IRequestHandler<CreateSubscriptionPaymentCommand, BaseSubscriptionPaymentDto>
        {
            private readonly IMapper _mapper;
            private readonly IMediator _mediator;
            private readonly ISubscriptionPaymentWriteRepository _subscriptionPaymentWriteRepository;
            private readonly IUserReadRepository _userReadRepository;
            private readonly SubscriptionPaymentBusinessRules _subscriptionPaymentBusinessRules;

            public CreateSubscriptionPaymentCommandHandler(ISubscriptionPaymentWriteRepository subscriptionPaymentWriteRepository, IMapper mapper,
                SubscriptionPaymentBusinessRules subscriptionPaymentBusinessRules, IMediator mediator, IUserReadRepository userReadRepository)
            {
                _mapper = mapper;
                _subscriptionPaymentBusinessRules = subscriptionPaymentBusinessRules;
                _mediator = mediator;
                _userReadRepository = userReadRepository;
                _subscriptionPaymentWriteRepository = subscriptionPaymentWriteRepository;
            }

            public async Task<BaseSubscriptionPaymentDto> Handle(CreateSubscriptionPaymentCommand request, CancellationToken cancellationToken)
            {
                User user = await _subscriptionPaymentBusinessRules.CheckIfUserDoesNotExistsAndGetUser(request.UserId);
                await _subscriptionPaymentBusinessRules.CheckIfSubscriptionPaymentAlreadyExists(request.UserId);

                var mappedSubscriptionPayment = _mapper.Map<SubscriptionPayment>(request);
                var createdSubscriptionPayment = await _subscriptionPaymentWriteRepository.AddAsync(mappedSubscriptionPayment);
                await AddRoleToUserAsync(user);
                return _mapper.Map<BaseSubscriptionPaymentDto>(createdSubscriptionPayment);
            }

            private async Task AddRoleToUserAsync(User userToAdd)
            {
                await _mediator.Send(new CreateUserOperationClaimCommand
                {
                    User = userToAdd,
                    OperationClaims = new HashSet<OperationClaim>() { new() { Name = OperationClaimsEnum.SubscriptionPayment.ToString() } }
                });
            }
        }
    }
}
