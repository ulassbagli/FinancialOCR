using Core.Security.Entities;
using Project.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Application.Features.Customers.Dtos.BaseDto
{
    public class BaseCustomerDto
    {
        public Guid Id { get; set; }
        public virtual User User { get; set; }
        public virtual Invoice Invoice { get; set; }
    }
}
