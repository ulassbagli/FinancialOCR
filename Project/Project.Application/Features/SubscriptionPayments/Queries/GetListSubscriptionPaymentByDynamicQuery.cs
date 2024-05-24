using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Core.Application.Requests;
using Core.Persistence.Dynamic;
using Core.Security.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Project.Application.Features.SubscriptionPayments.Models;
using Project.Application.Services.Repositories.SubscriptionPayments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Application.Features.SubscriptionPayments.Queries
{
    public class GetListSubscriptionPaymentByDynamicQuery : IRequest<SubscriptionPaymentListModel>, ISecuredRequest
    {
        public Dynamic Dynamic { get; set; }
        public PageRequest PageRequest { get; set; }

        public string[] Roles { get; } = { OperationClaimsEnum.SubscriptionPayment.ToString() };

        public class GetListSubscriptionPaymentByDynamicQueryHandler : IRequestHandler<GetListSubscriptionPaymentByDynamicQuery, SubscriptionPaymentListModel>
        {
            private readonly ISubscriptionPaymentReadRepository _subscriptionPaymentReadRepository;
            private readonly IMapper _mapper;

            public GetListSubscriptionPaymentByDynamicQueryHandler(IMapper mapper, ISubscriptionPaymentReadRepository subscriptionPaymentReadRepository)
            {
                _subscriptionPaymentReadRepository = subscriptionPaymentReadRepository;
                _mapper = mapper;
            }

            public async Task<SubscriptionPaymentListModel> Handle(GetListSubscriptionPaymentByDynamicQuery request, CancellationToken cancellationToken)
            {
                var models = await _subscriptionPaymentReadRepository.GetListByDynamicAsync(request.Dynamic, include:
                    m => m.Include(c => c.User),
                    index: request.PageRequest.Page,
                    size: request.PageRequest.PageSize,
                    cancellationToken: cancellationToken);
                return _mapper.Map<SubscriptionPaymentListModel>(models);
            }
        }
    }
}
