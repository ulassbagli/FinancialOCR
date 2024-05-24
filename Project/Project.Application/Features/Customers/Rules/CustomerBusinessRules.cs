using Application.Services.Repositories.Users;
using Core.CrossCuttingConcerns.Exceptions;
using Core.Security.Entities;
using Domain.Entities;
using Project.Application.Features.Customers.Constants;
using Project.Application.Services.Repositories.Customers;
using Project.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Application.Features.Customers.Rules
{
    public class CustomerBusinessRules
    {
        private readonly ICustomerReadRepository _customerReadRepository;
        private readonly IUserReadRepository _userReadRepository;

        public CustomerBusinessRules(ICustomerReadRepository customerReadRepository, IUserReadRepository userReadRepository)
        {
            _customerReadRepository = customerReadRepository;
            _userReadRepository = userReadRepository;
        }
        public async Task CheckIfCustomerAlreadyExists(string userId)
        {
            var customer = await _customerReadRepository.GetAsync(b => b.UserId == Guid.Parse(userId));
            if (customer is not null) throw new BusinessException(CustomerMessages.CustomerAlreadyExists); //TODO: Localize message.
        }
        public async Task<User> CheckIfUserDoesNotExistsAndGetUser(string userId)
        {
            var user = await _userReadRepository.GetByIdAsync(userId);
            if (user is null) throw new BusinessException(CustomerMessages.UserNotFound); //TODO: Localize message.
            return user;
        }
        public async Task CheckIfCustomerDoesNotExists(Customer customer)
        {
            if (customer == null) throw new BusinessException(CustomerMessages.CustomerNotFound); //TODO: Localize message.
        }
        public async Task<Customer> CheckIfCustomerDoesNotExistsAndGetCustomer(string customerId)
        {
            var customer = await _customerReadRepository.GetByIdAsync(customerId);
            if (customer == null) throw new BusinessException(CustomerMessages.CustomerNotFound); //TODO: Localize message.
            return customer;
        }
    }
}
