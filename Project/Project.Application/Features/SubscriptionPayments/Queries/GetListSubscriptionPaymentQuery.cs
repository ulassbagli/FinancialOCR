using AutoMapper;
using Core.Application.Requests;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Project.Application.Features.SubscriptionPayments.Dtos;
using Project.Application.Features.SubscriptionPayments.Models;
using Project.Application.Services.Repositories.SubscriptionPayments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Application.Features.SubscriptionPayments.Queries
{
    public class GetListSubscriptionPaymentQuery : IRequest<SubscriptionPaymentListModel>
    {
        public PageRequest PageRequest { get; set; }

        public class GetListSubscriptionPaymentQueryHandler : IRequestHandler<GetListSubscriptionPaymentQuery, SubscriptionPaymentListModel>
        {
            private readonly IMapper _mapper;
            private readonly ISubscriptionPaymentReadRepository _subscriptionPaymentReadRepository;

            public GetListSubscriptionPaymentQueryHandler(ISubscriptionPaymentReadRepository subscriptionPaymentReadRepository, IMapper mapper)
            {
                _mapper = mapper;
                _subscriptionPaymentReadRepository = subscriptionPaymentReadRepository;
            }

            public async Task<SubscriptionPaymentListModel> Handle(GetListSubscriptionPaymentQuery request, CancellationToken cancellationToken)
            {
                var subscriptionPayments = await _subscriptionPaymentReadRepository.GetListAsync(include: m => m.Include(p => p.User),
                    index: request.PageRequest.Page, size: request.PageRequest.PageSize);
                return _mapper.Map<SubscriptionPaymentListModel>(subscriptionPayments);
            }
        }
    }
}
