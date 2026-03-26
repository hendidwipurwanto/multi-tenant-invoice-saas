using MediatR;
using MultiTenantInvoice.Application.Common.Interfaces;
using MultiTenantInvoice.Domain.Entities;
using MultiTenantInvoice.Domain.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiTenantInvoice.Application.Features.AuditLogs.EventHandlers
{
    public class InvoiceCreatedAuditLogHandler : INotificationHandler<InvoiceCreatedEvent>
    {
        private readonly IAppDbContext _dbContext;

        public InvoiceCreatedAuditLogHandler(IAppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Handle(
            InvoiceCreatedEvent notification,
            CancellationToken cancellationToken)
        {
            var audit = new AuditLog
            {
                Id = Guid.NewGuid(),
                Action = "CREATE_INVOICE",
                EntityName = "Invoice",
                EntityId = notification.InvoiceId.ToString(),
                Description = $"Invoice {notification.InvoiceId} created",
                CreatedAt = DateTime.UtcNow
            };

            _dbContext.AuditLogs.Add(audit);

            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
