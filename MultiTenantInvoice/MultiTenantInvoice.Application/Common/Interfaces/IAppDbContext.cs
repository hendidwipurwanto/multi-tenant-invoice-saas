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

        public DbSet<AuditLog> AuditLogs { get; set; }

        public DbSet<Payment> Payments { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
