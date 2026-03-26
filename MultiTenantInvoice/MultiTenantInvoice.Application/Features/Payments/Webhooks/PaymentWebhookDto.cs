using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiTenantInvoice.Application.Features.Payments.Webhooks
{
    public class PaymentWebhookDto
    {
        public string EventType { get; set; }

        public Guid PaymentId { get; set; }

        public Guid InvoiceId { get; set; }
    }
}
