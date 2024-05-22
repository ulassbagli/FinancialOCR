using Application.Features.Developers.Constants;
using Application.Services.Repositories.Developers;
using Application.Services.Repositories.Users;
using Core.CrossCuttingConcerns.Exceptions;
using Core.Security.Entities;
using Domain.Entities;
using Project.Application.Features.Companies.Constants;
using Project.Application.Services.Repositories.Companies;
using Project.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Application.Features.Companies.Rules
{
    public class CompanyBusinessRules
    {
        private readonly ICompanyReadRepository _companyReadRepository;
        private readonly IUserReadRepository    _userReadRepository;

        public CompanyBusinessRules(ICompanyReadRepository companyReadRepository, IUserReadRepository userReadRepository)
        {
            _companyReadRepository = companyReadRepository;
            _userReadRepository = userReadRepository;
        }
        public async Task CheckIfCompanyAlreadyExists(string userId)
        {
            var company = await _companyReadRepository.GetAsync(b => b.UserId == Guid.Parse(userId));
            if (company is not null) throw new BusinessException(CompanyMessages.CompanyAlreadyExists); //TODO: Localize message.
        }
        public async Task<User> CheckIfUserDoesNotExistsAndGetUser(string userId)
        {
            var user = await _userReadRepository.GetByIdAsync(userId);
            if (user is null) throw new BusinessException(CompanyMessages.UserNotFound); //TODO: Localize message.
            return user;
        }
        public async Task CheckIfCompanyDoesNotExists(Company company)
        {
            if (company == null) throw new BusinessException(CompanyMessages.CompanyNotFound); //TODO: Localize message.
        }
        public async Task<Company> CheckIfCompanyDoesNotExistsAndGetCompany(string companyId)
        {
            var company = await _companyReadRepository.GetByIdAsync(companyId);
            if (company == null) throw new BusinessException(CompanyMessages.CompanyNotFound); //TODO: Localize message.
            return company;
        }
    }
}
