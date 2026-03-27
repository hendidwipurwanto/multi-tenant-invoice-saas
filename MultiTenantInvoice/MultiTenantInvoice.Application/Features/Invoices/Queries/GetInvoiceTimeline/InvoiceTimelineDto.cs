using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiTenantInvoice.Application.Features.Invoices.Queries.GetInvoiceTimeline
{
    public class InvoiceTimelineDto
    {
        public string EventType { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
