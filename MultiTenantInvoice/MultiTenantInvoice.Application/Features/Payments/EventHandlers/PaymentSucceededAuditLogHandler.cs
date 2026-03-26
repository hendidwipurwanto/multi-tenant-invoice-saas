using MediatR;
using MultiTenantInvoice.Application.Common.Interfaces;
using MultiTenantInvoice.Domain.Entities;
using MultiTenantInvoice.Domain.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MultiTenantInvoice.Application.Features.Payments.EventHandlers
{
    public class PaymentSucceededAuditLogHandler
      : INotificationHandler<PaymentSucceededEvent>
    {
        private readonly IAppDbContext _dbContext;

        public PaymentSucceededAuditLogHandler(IAppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Handle(
            PaymentSucceededEvent notification,
            CancellationToken cancellationToken)
        {
            var metadata = JsonSerializer.Serialize(new
            {
                paymentId = notification.PaymentId,
                invoiceId = notification.InvoiceId
            });

            var audit = new AuditLog
            {
                Id = Guid.NewGuid(),
                EventType = "payment.succeeded",
                EntityName = "Invoice",
                EntityId = notification.InvoiceId.ToString(),
                Metadata = metadata,
                CreatedAt = DateTime.UtcNow
            };

            _dbContext.AuditLogs.Add(audit);

            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
