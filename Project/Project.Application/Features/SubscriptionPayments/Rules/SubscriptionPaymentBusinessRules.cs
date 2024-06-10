using Application.Services.Repositories.Users;
using Core.CrossCuttingConcerns.Exceptions;
using Core.Security.Entities;
using Domain.Entities;
using Project.Application.Features.SubscriptionPayments.Constants;
using Project.Application.Services.Repositories.SubscriptionPayments;
using Project.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Application.Features.SubscriptionPayments.Rules
{
    public class SubscriptionPaymentBusinessRules
    {
        private readonly ISubscriptionPaymentReadRepository _subscriptionPaymentReadRepository;
        private readonly IUserReadRepository _userReadRepository;

        public SubscriptionPaymentBusinessRules(ISubscriptionPaymentReadRepository subscriptionPaymentReadRepository, IUserReadRepository userReadRepository)
        {
            _subscriptionPaymentReadRepository = subscriptionPaymentReadRepository;
            _userReadRepository = userReadRepository;
        }
        public async Task CheckIfSubscriptionPaymentAlreadyExists(string userId)
        {
            var subscriptionPayment = await _subscriptionPaymentReadRepository.GetAsync(b => b.Id == Guid.Parse(userId));
            if (subscriptionPayment is not null) throw new BusinessException(SubscriptionPaymentMessages.SubscriptionPaymentAlreadyExists); //TODO: Localize message.
        }
        public async Task<User> CheckIfUserDoesNotExistsAndGetUser(string userId)
        {
            var user = await _userReadRepository.GetByIdAsync(userId);
            if (user is null) throw new BusinessException(SubscriptionPaymentMessages.UserNotFound); //TODO: Localize message.
            return user;
        }
        public async Task CheckIfSubscriptionPaymentDoesNotExists(SubscriptionPayment subscriptionPayment)
        {
            if (subscriptionPayment == null) throw new BusinessException(SubscriptionPaymentMessages.SubscriptionPaymentNotFound); //TODO: Localize message.
        }
        public async Task<SubscriptionPayment> CheckIfSubscriptionPaymentDoesNotExistsAndGetSubscriptionPayment(string subscriptionPaymentId)
        {
            var subscriptionPayment = await _subscriptionPaymentReadRepository.GetByIdAsync(subscriptionPaymentId);
            if (subscriptionPayment == null) throw new BusinessException(SubscriptionPaymentMessages.SubscriptionPaymentNotFound); //TODO: Localize message.
            return subscriptionPayment;
        }
    }
}
