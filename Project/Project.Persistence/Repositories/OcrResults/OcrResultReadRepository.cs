using Core.Persistence.Repositories;
using Project.Application.Services.Repositories.OcrResults;
using Project.Domain.Entities;
using Project.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Persistence.Repositories.OcrResults
{
    public class OcrResultReadRepository : ReadRepository<OcrResult, FinancialOCRDbContext>, IOcrResultReadRepository
    {
        public OcrResultReadRepository(FinancialOCRDbContext context) : base(context)
        {
        }
    }
}
