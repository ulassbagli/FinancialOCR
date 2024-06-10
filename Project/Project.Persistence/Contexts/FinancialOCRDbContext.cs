using Core.Security.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Project.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using File = Project.Domain.Entities.File;

namespace Project.Persistence.Contexts
{
    public class FinancialOCRDbContext : DbContext
    {
        public FinancialOCRDbContext(DbContextOptions<FinancialOCRDbContext> options) : base(options) { }

        public DbSet<Company> Companies { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Accountant> Accountants { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<OcrResult> ocrResults { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<File> Files { get; set; }
        public DbSet<SubscriptionPayment> subscriptionPayments { get; set; }
        public DbSet<RefreshToken> refreshTokens { get; set; }
        public DbSet<OperationClaim> OperationClaims { get; set; }
        public DbSet<UserOperationClaim> UserOperationClaims { get; set; }


    }



}
