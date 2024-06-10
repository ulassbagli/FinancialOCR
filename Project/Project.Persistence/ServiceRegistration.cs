using Core.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Project.Application.Services;
using Project.Application.Services.Repositories.Accountants;
using Project.Application.Services.Repositories.Companies;
using Project.Application.Services.Repositories.Customers;
using Project.Application.Services.Repositories.Files;
using Project.Application.Services.Repositories.ImageUploads;
using Project.Application.Services.Repositories.Invoices;
using Project.Application.Services.Repositories.OcrResults;
using Project.Application.Services.Repositories.SubscriptionPayments;
using Project.Domain.Entities;
using Project.Persistence.Contexts;
using Project.Persistence.Repositories.Accountants;
using Project.Persistence.Repositories.Companies;
using Project.Persistence.Repositories.Customers;
using Project.Persistence.Repositories.Files;
using Project.Persistence.Repositories.ImageUploads;
using Project.Persistence.Repositories.Invoices;
using Project.Persistence.Repositories.OcrResults;
using Project.Persistence.Repositories.SubscriptionPayments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using File = Project.Domain.Entities.File;

namespace Project.Persistence
{
    public static class ServiceRegistration
    {
        public static void AddPersistenceServices(this IServiceCollection services)
        {
            services.AddDbContext<FinancialOCRDbContext>(options => options.UseSqlServer(Configuration.ConnectionString));


            services.AddScoped<ICustomerReadRepository, CustomerReadRepository>();
            services.AddScoped<ICustomerWriteRepository, CustomerWriteRepository>();
            
            services.AddScoped<IAccountantReadRepository, AccountantReadRepository>();
            services.AddScoped<IAccountantWriteRepository, AccountantWriteRepository>();

            services.AddScoped<ICompanyReadRepository, CompanyReadRepository>();
            services.AddScoped<ICompanyWriteRepository, CompanyWriteRepository>();

            services.AddScoped<IFileReadRepository, FileReadRepository>();
            services.AddScoped<IFileWriteRepository, FileWriteRepository>();

            services.AddScoped<IInvoiceReadRepository, InvoiceReadRepository>();
            services.AddScoped<IInvoiceWriteRepository, InvoiceWriteRepository>();

            services.AddScoped<IOcrResultReadRepository, OcrResultReadRepository>();
            services.AddScoped<IOcrResultWriteRepository, OcrResultWriteRepository>();

            services.AddScoped<ISubscriptionPaymentReadRepository, SubscriptionPaymentReadRepository>();
            services.AddScoped<ISubscriptionPaymentWriteRepository, SubscriptionPaymentWriteRepository>();

        }
    }
}
