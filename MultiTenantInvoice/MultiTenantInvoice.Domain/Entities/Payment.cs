using MultiTenantInvoice.Domain.Enums;
using MultiTenantInvoice.Domain.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiTenantInvoice.Domain.Entities
{
    public class Payment : BaseEntity
    {
        public Guid InvoiceId { get; set; }

        public Invoice Invoice { get; set; } = null!;

        public decimal Amount { get; set; }

        public PaymentMethod Method { get; set; }

        public PaymentStatus Status { get; set; }

        public DateTime AttemptedAt { get; set; }

        public void MarkSuccess()
        {
            Status = PaymentStatus.Success;

            AddDomainEvent(new PaymentSucceededEvent(Id, InvoiceId));
        }
    }
}
