using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiTenantInvoice.Domain.Events
{
    public class InvoiceCreatedEvent : IDomainEvent
    {
        public Guid InvoiceId { get; }

        public DateTime OccurredOn { get; } = DateTime.UtcNow;

        public InvoiceCreatedEvent(Guid invoiceId)
        {
            InvoiceId = invoiceId;
        }
    }
}
