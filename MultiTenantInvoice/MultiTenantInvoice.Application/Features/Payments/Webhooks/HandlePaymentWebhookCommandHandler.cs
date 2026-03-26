using MediatR;
using MultiTenantInvoice.Application.Common.Interfaces;
using MultiTenantInvoice.Domain.Enums;
using MultiTenantInvoice.Domain.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiTenantInvoice.Application.Features.Payments.Webhooks
{
    public class HandlePaymentWebhookCommandHandler : IRequestHandler<HandlePaymentWebhookCommand>
    {
        private readonly IAppDbContext _dbContext;

        public HandlePaymentWebhookCommandHandler(IAppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Handle(HandlePaymentWebhookCommand request, CancellationToken cancellationToken)
        {
            var payment = await _dbContext.Payments.FindAsync(request.PaymentId);

            if (payment == null)
                return;

            if (request.EventType == "payment.succeeded")
            {
                payment.Status = PaymentStatus.Success;

                payment.AddDomainEvent(
                    new PaymentSucceededEvent(payment.Id, payment.InvoiceId));

                var invoice = await _dbContext.Invoices.FindAsync(payment.InvoiceId);

                if (invoice != null)
                    invoice.Status = InvoiceStatus.Paid;
            }

            if (request.EventType == "payment.failed")
            {
                payment.Status = PaymentStatus.Failed;
            }

            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
