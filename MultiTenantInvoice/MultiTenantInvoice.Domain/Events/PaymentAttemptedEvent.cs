using MultiTenantInvoice.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiTenantInvoice.Domain.Events
{
    public class PaymentAttemptedEvent : IDomainEvent
    {
        public Guid PaymentId { get; }

        public Guid InvoiceId { get; }

        public DateTime OccurredOn { get; } = DateTime.UtcNow;

        public PaymentAttemptedEvent(Guid paymentId, Guid invoiceId)
        {
            PaymentId = paymentId;
            InvoiceId = invoiceId;
        }
    }
}
