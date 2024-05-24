using AutoMapper;
using Core.Application.Pipelines.Authorization;
using MediatR;
using Project.Application.Features.SubscriptionPayments.Dtos.BaseDto;
using Project.Application.Features.SubscriptionPayments.Rules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Application.Features.SubscriptionPayments.Queries
{
    public class GetByIdSubscriptionPaymentQuery : IRequest<BaseSubscriptionPaymentDto>
    {
        public string Id { get; set; }

        public class GetByIdSubscriptionPaymentQueryHandler : IRequestHandler<GetByIdSubscriptionPaymentQuery, BaseSubscriptionPaymentDto>
        {
            private readonly SubscriptionPaymentBusinessRules _subscriptionPaymentBusinessRules;
            private readonly IMapper _mapper;

            public GetByIdSubscriptionPaymentQueryHandler(IMapper mapper, SubscriptionPaymentBusinessRules subscriptionPaymentBusinessRules)
            {
                _subscriptionPaymentBusinessRules = subscriptionPaymentBusinessRules;
                _mapper = mapper;
            }

            public async Task<BaseSubscriptionPaymentDto> Handle(GetByIdSubscriptionPaymentQuery request, CancellationToken cancellationToken)
            {
                var subscriptionPayment = await _subscriptionPaymentBusinessRules.CheckIfSubscriptionPaymentDoesNotExistsAndGetSubscriptionPayment(request.Id);

                return _mapper.Map<BaseSubscriptionPaymentDto>(subscriptionPayment);
            }
        }
    }
}
