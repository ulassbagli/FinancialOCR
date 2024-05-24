using Project.Application.Features.SubscriptionPayments.Dtos.BaseDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Application.Features.SubscriptionPayments.Dtos
{
    public class DeletedSubscriptionPaymentDto : BaseSubscriptionPaymentDto
    {
        public DateTime DeletedDate { get; set; }
        public bool isDeleted { get; set; }
    }
}
