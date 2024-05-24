using AutoMapper;
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

namespace Project.Application.Features.SubscriptionPayments.Command.UpdateCommand
{
    public class UpdateSubscriptionPaymentCommand : IRequest<BaseSubscriptionPaymentDto>
    {
        public string Id { get; set; }
        public string UserId { get; set; }

        public class UpdateSubscriptionPaymentCommandHandler : IRequestHandler<UpdateSubscriptionPaymentCommand, BaseSubscriptionPaymentDto>
        {
            private readonly ISubscriptionPaymentWriteRepository _subscriptionPaymentWriteRepository;
            private readonly SubscriptionPaymentBusinessRules _subscriptionPaymentBusinessRules;
            private readonly IMapper _mapper;

            public UpdateSubscriptionPaymentCommandHandler(IMapper mapper, ISubscriptionPaymentWriteRepository subscriptionPaymentWriteRepository, SubscriptionPaymentBusinessRules subscriptionPaymentBusinessRules)
            {
                _subscriptionPaymentWriteRepository = subscriptionPaymentWriteRepository;
                _subscriptionPaymentBusinessRules = subscriptionPaymentBusinessRules;
                _mapper = mapper;
            }

            public async Task<BaseSubscriptionPaymentDto> Handle(UpdateSubscriptionPaymentCommand request, CancellationToken cancellationToken)
            {
                var subscriptionPaymentToUpdate = await _subscriptionPaymentBusinessRules.CheckIfSubscriptionPaymentDoesNotExistsAndGetSubscriptionPayment(request.Id);
                await _subscriptionPaymentBusinessRules.CheckIfUserDoesNotExistsAndGetUser(request.UserId);

                _mapper.Map(request, subscriptionPaymentToUpdate, typeof(UpdateSubscriptionPaymentCommand), typeof(SubscriptionPayment));
                await _subscriptionPaymentWriteRepository.Update(subscriptionPaymentToUpdate);
                return _mapper.Map<BaseSubscriptionPaymentDto>(subscriptionPaymentToUpdate);
            }
        }
    }
}
