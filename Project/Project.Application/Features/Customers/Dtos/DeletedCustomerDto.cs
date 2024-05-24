using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Application.Features.Customers.Dtos
{
    public class DeletedCustomerDto
    {
        public DateTime DeletedDate { get; set; }
        public bool isDeleted { get; set; }
    }
}
