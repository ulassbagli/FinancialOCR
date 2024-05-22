using Core.Persistence.Repositories;
using Project.Application.Services.Repositories.Files;
using Project.Domain.Entities;
using Project.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using File = Project.Domain.Entities.File;

namespace Project.Persistence.Repositories.Files
{
    public class FileWriteRepository : WriteRepository<File, FinancialOCRDbContext>, IFileWriteRepository
    {
        public FileWriteRepository(FinancialOCRDbContext context) : base(context)
        {
        }
    }
}
