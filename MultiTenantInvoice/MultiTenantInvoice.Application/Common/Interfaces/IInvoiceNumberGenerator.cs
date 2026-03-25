using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiTenantInvoice.Application.Common.Interfaces
{
    public interface IInvoiceNumberGenerator
    {
        Task<string> GenerateAsync(CancellationToken cancellationToken);

    }
}
