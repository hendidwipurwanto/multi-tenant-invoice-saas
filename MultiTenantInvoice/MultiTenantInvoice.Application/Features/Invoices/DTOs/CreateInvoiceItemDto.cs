using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiTenantInvoice.Application.Features.Invoices.DTOs
{
    public class CreateInvoiceItemDto
    {
        public string Description { get; set; }

        public decimal Quantity { get; set; }

        public decimal UnitPrice { get; set; }
    }
}
