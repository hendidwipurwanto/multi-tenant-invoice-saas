using MediatR;
using MultiTenantInvoice.Application.Common.Interfaces;
using MultiTenantInvoice.Domain.Entities;
using MultiTenantInvoice.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiTenantInvoice.Application.Features.Invoices.Commands.CreateInvoice
{
    public class CreateInvoiceCommandHandler : IRequestHandler<CreateInvoiceCommand, Guid>
    {
        private readonly IAppDbContext _dbContext;
        private readonly ITenantProvider _tenantProvider;
        private readonly IInvoiceNumberGenerator _invoiceNumberGenerator;

        public CreateInvoiceCommandHandler(IAppDbContext dbContext, ITenantProvider tenantProvider, IInvoiceNumberGenerator invoiceNumberGenerator)
        {
            _dbContext = dbContext;
            _tenantProvider = tenantProvider;
            _invoiceNumberGenerator = invoiceNumberGenerator;
        }

        public async Task<Guid> Handle(
            CreateInvoiceCommand command,
            CancellationToken cancellationToken)
        {
            var request = command.Request;

            var invoiceNumber = await _invoiceNumberGenerator.GenerateAsync(cancellationToken);

            var invoice = new Invoice
            {
                Id = Guid.NewGuid(),
                InvoiceNumber = invoiceNumber,
                CustomerId = request.CustomerId,
                IssueDate = request.IssueDate,
                DueDate = request.DueDate,
                TenantId = _tenantProvider.GetTenantId(),
                Status = InvoiceStatus.Draft,
                CreatedAt = DateTime.UtcNow
            };

            foreach (var item in request.Items)
            {
                invoice.Items.Add(new InvoiceItem
                {
                    Id = Guid.NewGuid(),
                    Description = item.Description,
                    Quantity = item.Quantity,
                    UnitPrice = item.UnitPrice
                });
            }

            invoice.CalculateTotals();

            _dbContext.Invoices.Add(invoice);

            await _dbContext.SaveChangesAsync(cancellationToken);

            return invoice.Id;
        }
    }
}
