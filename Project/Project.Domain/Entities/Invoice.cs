using Core.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Domain.Entities
{
    public  class Invoice : Entity
    {
        public Guid Id { get; set; }
        public virtual File File { get; set; }
        public virtual Customer Customer { get; set; }
        public string InvoiceId { get; set; }
        public DateTime InvoiceDate { get; set; }
        public string type { get; set; }
        public decimal Amount { get; set; }
        public decimal TaxAmount { get; set; }
        public int TaxRate { get; set; }
    }
}
