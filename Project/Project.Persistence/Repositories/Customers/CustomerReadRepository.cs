using Core.Persistence.Repositories;
using Project.Application.Services.Repositories.Customers;
using Project.Domain.Entities;
using Project.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Persistence.Repositories.Customers
{
    public class CustomerReadRepository : ReadRepository<Customer, FinancialOCRDbContext>, ICustomerReadRepository
    {
        public CustomerReadRepository(FinancialOCRDbContext context) : base(context)
        {
        }
    }
}
