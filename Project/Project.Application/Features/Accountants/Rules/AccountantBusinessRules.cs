using Application.Services.Repositories.Users;
using Core.CrossCuttingConcerns.Exceptions;
using Core.Security.Entities;
using Domain.Entities;
using Project.Application.Features.Accountants.Constants;
using Project.Application.Services.Repositories.Accountants;
using Project.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Application.Features.Accountants.Rules
{
    public class AccountantBusinessRules
    {
        private readonly IAccountantReadRepository _accountantReadRepository;
        private readonly IUserReadRepository _userReadRepository;

        public AccountantBusinessRules(IAccountantReadRepository accountantReadRepository, IUserReadRepository userReadRepository)
        {
            _accountantReadRepository = accountantReadRepository;
            _userReadRepository = userReadRepository;
        }
        public async Task CheckIfAccountantAlreadyExists(string userId)
        {
            var Accountant = await _accountantReadRepository.GetAsync(b => b.Id == Guid.Parse(userId));
            if (Accountant is not null) throw new BusinessException(AccountantMessages.AccountantAlreadyExists); //TODO: Localize message.
        }
        
        public async Task<Accountant> CheckIfAccountantDoesNotExistsAndGetAccountant(string AccountantId)
        {
            var Accountant = await _accountantReadRepository.GetByIdAsync(AccountantId);
            if (Accountant == null) throw new BusinessException(AccountantMessages.AccountantNotFound); //TODO: Localize message.
            return Accountant;
        }
    }
}
