using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Application.Features.SubscriptionPayments.Dtos.BaseDto
{
    public class BaseSubscriptionPaymentDto
    {
        public Guid Id { get; set; }
        public decimal Amount { get; set; }
        public DateTime paymentDate { get; set; }
        public string subscriptionType { get; set; }
    }
}
