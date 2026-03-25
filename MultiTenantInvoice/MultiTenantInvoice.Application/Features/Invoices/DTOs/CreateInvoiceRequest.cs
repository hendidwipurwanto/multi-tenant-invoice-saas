using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiTenantInvoice.Application.Features.Invoices.DTOs
{
    public class CreateInvoiceRequest
    {
        public Guid CustomerId { get; set; }

        public DateTime IssueDate { get; set; }

        public DateTime DueDate { get; set; }

        public List<CreateInvoiceItemDto> Items { get; set; }
    }
}
