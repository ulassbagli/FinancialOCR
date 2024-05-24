using Application.Features.Developers.Dtos.BaseDto;
using Core.Persistence.Paging;
using Project.Application.Features.SubscriptionPayments.Dtos.BaseDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Application.Features.SubscriptionPayments.Models
{
    public class SubscriptionPaymentListModel : BasePageableModel
    {
        public IEnumerable<BaseSubscriptionPaymentDto> Items { get; set; }
    }
}
