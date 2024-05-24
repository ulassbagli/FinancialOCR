using Application.Services.Repositories.Users;
using Core.CrossCuttingConcerns.Exceptions;
using Core.Security.Entities;
using Domain.Entities;
using Project.Application.Features.Invoices.Constants;
using Project.Application.Services.Repositories.Invoices;
using Project.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Application.Features.Invoices.Rules
{
    public class InvoiceBusinessRules
    {
        private readonly IInvoiceReadRepository _invoiceReadRepository;
        private readonly IUserReadRepository _userReadRepository;

        public InvoiceBusinessRules(IInvoiceReadRepository invoiceReadRepository, IUserReadRepository userReadRepository)
        {
            _invoiceReadRepository = invoiceReadRepository;
            _userReadRepository = userReadRepository;
        }
        public async Task CheckIfInvoiceAlreadyExists(string userId)
        {
            var invoice = await _invoiceReadRepository.GetAsync(b => b.userId == Guid.Parse(userId));
            if (invoice is not null) throw new BusinessException(InvoiceMessages.InvoiceAlreadyExists); //TODO: Localize message.
        }
        public async Task<User> CheckIfUserDoesNotExistsAndGetUser(string userId)
        {
            var user = await _userReadRepository.GetByIdAsync(userId);
            if (user is null) throw new BusinessException(InvoiceMessages.UserNotFound); //TODO: Localize message.
            return user;
        }
        public async Task CheckIfInvoiceDoesNotExists(Invoice invoice)
        {
            if (invoice == null) throw new BusinessException(InvoiceMessages.InvoiceNotFound); //TODO: Localize message.
        }
        public async Task<Invoice> CheckIfInvoiceDoesNotExistsAndGetInvoice(string invoiceId)
        {
            var invoice = await _invoiceReadRepository.GetByIdAsync(invoiceId);
            if (invoice == null) throw new BusinessException(InvoiceMessages.InvoiceNotFound); //TODO: Localize message.
            return invoice;
        }
    }
}
