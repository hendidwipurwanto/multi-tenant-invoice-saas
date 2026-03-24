using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiTenantInvoice.Domain.Entities
{
    public class InvoiceItem
    {
        public Guid Id { get; set; }

        public Guid InvoiceId { get; set; }

        public string Description { get; set; }

        public decimal Quantity { get; set; }
        public decimal UnitPrice { get; set; }
    }
}
