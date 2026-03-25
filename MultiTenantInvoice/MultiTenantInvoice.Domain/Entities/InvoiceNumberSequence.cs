using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiTenantInvoice.Domain.Entities
{
    public class InvoiceNumberSequence : BaseEntity
    {
        public int Year { get; set; }

        public int LastNumber { get; set; }

    }
}
