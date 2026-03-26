using MediatR;
using Microsoft.EntityFrameworkCore;
using MultiTenantInvoice.Application.Common.Interfaces;
using MultiTenantInvoice.Application.Features.Payments.Queries.GetPaymentById;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiTenantInvoice.Application.Features.Payments.Queries
{
    public class GetPaymentByIdQueryHandler : IRequestHandler<GetPaymentByIdQuery, PaymentDto>
    {
        private readonly IAppDbContext _dbContext;

        public GetPaymentByIdQueryHandler(IAppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<PaymentDto> Handle(GetPaymentByIdQuery request,CancellationToken cancellationToken)
        {
            var payment = await _dbContext.Payments
                .FirstAsync(x => x.Id == request.PaymentId, cancellationToken);

            return new PaymentDto
            {
                Id = payment.Id,
                InvoiceId = payment.InvoiceId,
                Amount = payment.Amount,
                Method = payment.Method.ToString(),
                Status = payment.Status.ToString()
            };
        }
    }
}
