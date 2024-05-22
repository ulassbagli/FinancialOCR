using Core.Persistence.Repositories;
using Project.Application.Services.Repositories.Accountants;
using Project.Domain.Entities;
using Project.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Persistence.Repositories.Accountants
{
    public class AccountantReadRepository : ReadRepository<Accountant, FinancialOCRDbContext>, IAccountantReadRepository
    {
        public AccountantReadRepository(FinancialOCRDbContext context) : base(context)
        {
        }
    }
}
