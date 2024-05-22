using Core.Security.Entities;
using Microsoft.EntityFrameworkCore;
using Project.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Persistence.Contexts
{
    public class FinancialOCRDbContext : DbContext
    {
        public FinancialOCRDbContext(DbContextOptions<FinancialOCRDbContext> options) : base(options) { }


        public DbSet<Company> Company { get; set; }
        public DbSet<Customer> Customer { get; set; }
        public DbSet<Accountant> Accountant { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<RefreshToken> refreshTokens { get; set; }
        public DbSet<OperationClaim> OperationClaims { get; set; }
        public DbSet<UserOperationClaim> UserOperationClaims { get; set; }
        
    }
}
