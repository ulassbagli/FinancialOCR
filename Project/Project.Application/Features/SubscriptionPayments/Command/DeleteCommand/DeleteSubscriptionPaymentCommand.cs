using AutoMapper;
using Domain.Entities;
using MediatR;
using Project.Application.Features.Accountants.Dtos;
using Project.Application.Features.SubscriptionPayments.Dtos;
using Project.Application.Features.SubscriptionPayments.Dtos.BaseDto;
using Project.Application.Features.SubscriptionPayments.Rules;
using Project.Application.Services.Repositories.SubscriptionPayments;
using Project.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Application.Features.SubscriptionPayments.Command.DeleteCommand
{
    public class DeleteSubscriptionPaymentCommand : IRequest<DeletedSubscriptionPaymentDto>
    {
        public string Id { get; set; }
        public bool isSoftDelete { get; set; }

        public class DeleteSubscriptionPaymentCommandHandler : IRequestHandler<DeleteSubscriptionPaymentCommand, DeletedSubscriptionPaymentDto>
        {
            private readonly ISubscriptionPaymentWriteRepository _subscriptionPaymentWriteRepository;
            private readonly IMapper _mapper;
            private readonly SubscriptionPaymentBusinessRules _subscriptionPaymentBusinessRules;

            public DeleteSubscriptionPaymentCommandHandler(ISubscriptionPaymentWriteRepository subscriptionPaymentWriteRepository, IMapper mapper,
                SubscriptionPaymentBusinessRules subscriptionPaymentBusinessRules)
            {
                _mapper = mapper;
                _subscriptionPaymentBusinessRules = subscriptionPaymentBusinessRules;
                _subscriptionPaymentWriteRepository = subscriptionPaymentWriteRepository;
            }

            public async Task<DeletedSubscriptionPaymentDto> Handle(DeleteSubscriptionPaymentCommand request, CancellationToken cancellationToken)
            {
                var subscriptionPayment = await _subscriptionPaymentBusinessRules.CheckIfSubscriptionPaymentDoesNotExistsAndGetSubscriptionPayment(request.Id);

                SubscriptionPayment deletedSubscriptionPayment;
                if (request.isSoftDelete)
                    deletedSubscriptionPayment = await _subscriptionPaymentWriteRepository.SoftRemove(subscriptionPayment);
                else
                    deletedSubscriptionPayment = await _subscriptionPaymentWriteRepository.HardRemove(subscriptionPayment);

                return _mapper.Map<DeletedSubscriptionPaymentDto>(deletedSubscriptionPayment);
            }
        }
    }
}
