using Core.Persistence.Repositories;
using Project.Application.Services.Repositories.Companies;
using Project.Domain.Entities;
using Project.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Persistence.Repositories.Companies
{
    public class CompanyWriteRepository : WriteRepository<Company, FinancialOCRDbContext>, ICompanyWriteRepository
    {
        public CompanyWriteRepository(FinancialOCRDbContext context) : base(context)
        {
        }
    }
}
