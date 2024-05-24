using Core.Persistence.Paging;
using Project.Application.Features.Invoices.Dtos.BaseDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Application.Features.Invoices.Models
{
    public class InvoiceListModel : BasePageableModel
    {
        public IEnumerable<BaseInvoiceDto> Items { get; set; }
    }
}
