using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiTenantInvoice.Application.Common.Tenant
{
    public class TenantContext
    {
        public Guid TenantId { get; set; }
    }
}
