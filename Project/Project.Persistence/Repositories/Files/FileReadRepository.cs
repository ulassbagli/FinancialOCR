using Core.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Project.Application.Services.Repositories.Files;
using Project.Domain.Entities;
using Project.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using File = Project.Domain.Entities.File;

namespace Project.Persistence.Repositories.Files
{
    public class FileReadRepository : ReadRepository<File, FinancialOCRDbContext>, IFileReadRepository
    {
        private readonly FinancialOCRDbContext _context;

        public FileReadRepository(FinancialOCRDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<File> GetByIdAsync(Guid id)
        {
            return await _context.Set<File>().FindAsync(id);
        }

        public async Task<IEnumerable<File>> GetAllAsync()
        {
            return await _context.Set<File>().ToListAsync();
        }

        public async Task<IEnumerable<File>> GetFilterAsync(Expression<Func<File , bool>> predicate)
        {
            return await _context.Set<File>().Where(predicate).ToListAsync();
        }

        public async Task<File> GetByCustomerId(Guid id)
        {
            return await _context.Set<File>().FirstOrDefaultAsync();
        }
    }
}
