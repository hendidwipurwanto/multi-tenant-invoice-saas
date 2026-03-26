using MediatR;
using MultiTenantInvoice.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiTenantInvoice.Application.Features.Payments.Commands.AttemptPayment
{
    public class AttemptPaymentCommand : IRequest<Guid>
    {
        public Guid InvoiceId { get; set; }

        public decimal Amount { get; set; }

        public PaymentMethod Method { get; set; }
    }
}
