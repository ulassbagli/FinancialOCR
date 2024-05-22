using Core.Security.Entities;
using Project.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Application.Features.Companies.Dtos.BaseDto
{
    public class BaseCompanyDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string TaxNo { get; set; }
        public virtual User User { get; set; }
    }
}
