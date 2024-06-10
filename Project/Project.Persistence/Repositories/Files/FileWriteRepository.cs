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
        private readonly FinancialOCRDbContext _context;

        public FileWriteRepository(FinancialOCRDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task AddAsync(File entity)
        {
            await _context.Set<File>().AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpadateAsync(File entity)
        {
            _context.Set<File>().Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(File entity)
        {
            _context.Set<File>().Remove(entity);
            await _context.SaveChangesAsync();
        }
    }
}
