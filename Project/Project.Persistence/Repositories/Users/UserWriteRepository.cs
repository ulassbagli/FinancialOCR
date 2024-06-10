using Application.Services.Repositories.Users;
using Core.Persistence.Repositories;
using Core.Security.Entities;
using Project.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Persistence.Repositories.Users
{
    public class UserWriteRepository : WriteRepository<User, FinancialOCRDbContext>, IUserWriteRepository
    {
        public UserWriteRepository(FinancialOCRDbContext context) : base(context)
        {
        }
    }
}
