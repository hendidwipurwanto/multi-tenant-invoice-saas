using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiTenantInvoice.Application.Features.Invoices.Queries.GetInvoices
{
    public class InvoiceListDto
    {
        public Guid Id { get; set; }

        public string InvoiceNumber { get; set; }

        public decimal Total { get; set; }

        public string Status { get; set; }

        public DateTime IssueDate { get; set; }
    }
}
