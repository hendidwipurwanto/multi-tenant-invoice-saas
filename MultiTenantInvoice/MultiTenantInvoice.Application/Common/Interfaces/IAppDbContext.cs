using Microsoft.EntityFrameworkCore;
using MultiTenantInvoice.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiTenantInvoice.Application.Common.Interfaces
{
    public interface IAppDbContext
    {
        DbSet<Invoice> Invoices { get; }

        DbSet<InvoiceItem> InvoiceItems { get; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
