using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiTenantInvoice.Domain.Common
{
    public interface IMultiTenantEntity
    {
        Guid TenantId { get; set; }
    }
}
