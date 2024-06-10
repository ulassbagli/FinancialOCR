using Core.Persistence.Repositories;
using Core.Security.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Domain.Entities
{
    public class Accountant : Entity
    {
        public Guid Id { get; set; }
        public virtual User User { get; set; }

        public ICollection<Customer> Customer { get; set; }
    }
}
