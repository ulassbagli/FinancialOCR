using Core.Persistence.Repositories;
using Project.Domain.Entities;
using Project.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Project.Application.Services.Repositories.Customers;

namespace Project.Persistence.Repositories.Customers
{
    public class CustomerWriteRepository : WriteRepository<Customer, FinancialOCRDbContext>, ICustomerWriteRepository
    {
        public CustomerWriteRepository(FinancialOCRDbContext context) : base(context)
        {
        }
    }
}
