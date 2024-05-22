using Core.Persistence.Repositories;
using Project.Application.Services.Repositories.SubscriptionPayments;
using Project.Domain.Entities;
using Project.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Persistence.Repositories.SubscriptionPayments
{
    public class SubscriptionPaymentWriteRepository : WriteRepository<SubscriptionPayment, FinancialOCRDbContext>, ISubscriptionPaymentWriteRepository
    {
        public SubscriptionPaymentWriteRepository(FinancialOCRDbContext context) : base(context)
        {
        }
    }
}
