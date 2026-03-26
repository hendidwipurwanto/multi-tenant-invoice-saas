using MultiTenantInvoice.Domain.Enums;
using MultiTenantInvoice.Domain.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiTenantInvoice.Domain.Entities
{
    public class Invoice : BaseEntity
    {
        public string InvoiceNumber { get; set; }

        public Guid CustomerId { get; set; }
        public Customer Customer { get; set; }

        public DateTime IssueDate { get; set; }
        public DateTime DueDate { get; set; }

        public decimal Subtotal { get; private set; }
        public decimal Tax { get; private set; }
        public decimal Total { get; private set; }

        public InvoiceStatus Status { get; set; }

        public ICollection<InvoiceItem> Items { get; set; } = new List<InvoiceItem>();

        public void CalculateTotals()
        {
            Subtotal = Items.Sum(x => x.Quantity * x.UnitPrice);
            Tax = Subtotal * 0.11m; // example
            Total = Subtotal + Tax;
        }

        // ===============================
        // DOMAIN EVENT
        // ===============================
         public void RaiseCreatedEvent()
        {
            AddDomainEvent(new InvoiceCreatedEvent(Id));
        }
    }
}
