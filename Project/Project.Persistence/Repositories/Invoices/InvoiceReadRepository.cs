using Core.Persistence.Repositories;
using Project.Application.Services.Repositories.Invoices;
using Project.Domain.Entities;
using Project.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Persistence.Repositories.Invoices
{
    public class InvoiceReadRepository : ReadRepository<Invoice, FinancialOCRDbContext>, IInvoiceReadRepository
    {
        public InvoiceReadRepository(FinancialOCRDbContext context) : base(context)
        {
        }
    }
}
