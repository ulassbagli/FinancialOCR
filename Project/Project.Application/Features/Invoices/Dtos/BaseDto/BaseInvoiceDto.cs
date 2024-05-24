using Project.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Application.Features.Invoices.Dtos.BaseDto
{
    public class BaseInvoiceDto
    {
        public Guid Id { get; set; }
        public string InvoiceId { get; set; }
        public DateTime InvoiceDate { get; set; }
        public string type { get; set; }
        public decimal Amount { get; set; }
        public decimal TaxAmount { get; set; }
        public int TaxRate { get; set; 
    }
}
