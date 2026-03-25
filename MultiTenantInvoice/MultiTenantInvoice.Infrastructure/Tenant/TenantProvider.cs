using MultiTenantInvoice.Application.Common.Interfaces;
using MultiTenantInvoice.Application.Common.Tenant;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiTenantInvoice.Infrastructure.Tenant
{
    public class TenantProvider : ITenantProvider
    {
        private readonly TenantContext _tenantContext;

        public TenantProvider(TenantContext tenantContext)
        {
            _tenantContext = tenantContext;
        }

        public Guid GetTenantId()
        {
            return _tenantContext.TenantId;
        }
    }
}
