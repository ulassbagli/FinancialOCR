using Core.Persistence.Paging;
using Project.Application.Features.Customers.Dtos.BaseDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Application.Features.Customers.Models
{
    public class CustomerListModel : BasePageableModel
    {
        public IEnumerable<BaseCustomerDto> Items { get; set; }
    }
}
