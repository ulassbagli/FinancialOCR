using Application.Features.Authentication.Rules;
using Application.Features.Developers.Rules;
using Application.Features.Frameworks.Rules;
using Application.Features.ProgrammingLanguages.Rules;
using Application.Features.SocialMedias.Rules;
using Core.Application.Pipelines.Authorization;
using Core.Application.Pipelines.Caching;
using Core.Application.Pipelines.Logging;
using Core.Application.Pipelines.Validation;
using Core.Security.JWT;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Project.Application.Features.Accountants.Rules;
using Project.Application.Features.Companies.Rules;
using Project.Application.Features.Customers.Rules;
using Project.Application.Features.Files.Rules;
using Project.Application.Features.Invoices.Rules;
using Project.Application.Features.OcrResults.Rules;
using Project.Application.Features.SubscriptionPayments.Rules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Project.Application
{
    public static class ApplicationServiceRegistration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddMediatR(Assembly.GetExecutingAssembly());

            services.AddScoped<ProgrammingLanguageBusinessRules>();
            services.AddScoped<FrameworkBusinessRules>();
            services.AddScoped<DeveloperBusinessRules>();
            services.AddScoped<AuthenticationBusinessRules>();
            services.AddScoped<SocialMediaBusinessRules>();
            services.AddScoped<CustomerBusinessRules>();
            services.AddScoped<AccountantBusinessRules>();
            services.AddScoped<CompanyBusinessRules>();
            services.AddScoped<FileBusinessRules>();
            services.AddScoped<InvoiceBusinessRules>();
            services.AddScoped<OcrResultBusinessRules>();
            services.AddScoped<SubscriptionPaymentBusinessRules>();

            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(AuthorizationBehavior<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(CachingBehavior<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(CacheRemovingBehavior<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestValidationBehavior<,>));

            services.AddTransient<ITokenHelper, JwtHelper>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            return services;

        }
    }
}
