using Project.Application.Features.Invoices.Dtos.BaseDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Application.Features.Invoices.Dtos
{
    public class DeletedInvoiceDto : BaseInvoiceDto
    {
        public DateTime DeletedDate { get; set; }
        public bool isDeleted { get; set; }
    }
}
