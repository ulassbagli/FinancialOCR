using Core.Security.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Application.Features.Accountants.Dtos.BaseDto
{
    public class BaseAccountantDto
    {
        public Guid Id { get; set; }
        public virtual User user { get; set; }
    }
}
