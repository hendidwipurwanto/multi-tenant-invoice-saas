using MediatR;
using MultiTenantInvoice.Application.Common.Interfaces;
using MultiTenantInvoice.Domain.Entities;
using MultiTenantInvoice.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiTenantInvoice.Application.Features.Payments.Commands.AttemptPayment
{
    public class AttemptPaymentCommandHandler
     : IRequestHandler<AttemptPaymentCommand, Guid>
    {
        private readonly IAppDbContext _dbContext;
        private readonly IPaymentProcessor _paymentProcessor;

        public AttemptPaymentCommandHandler(
            IAppDbContext dbContext,
            IPaymentProcessor paymentProcessor)
        {
            _dbContext = dbContext;
            _paymentProcessor = paymentProcessor;
        }

        public async Task<Guid> Handle(
            AttemptPaymentCommand request,
            CancellationToken cancellationToken)
        {
            var payment = new Payment
            {
                Id = Guid.NewGuid(),
                InvoiceId = request.InvoiceId,
                Amount = request.Amount,
                Method = request.Method,
                Status = PaymentStatus.Pending,
                AttemptedAt = DateTime.UtcNow
            };

            _dbContext.Payments.Add(payment);

            var success = await _paymentProcessor.ProcessPayment(request.Amount);

            payment.Status = success
                ? PaymentStatus.Success
                : PaymentStatus.Failed;

            if (success)
            {
                var invoice = await _dbContext.Invoices.FindAsync(request.InvoiceId);

                if (invoice != null)
                {
                    invoice.Status = InvoiceStatus.Paid;
                }
            }

            await _dbContext.SaveChangesAsync(cancellationToken);

            return payment.Id;
        }
    }
}
