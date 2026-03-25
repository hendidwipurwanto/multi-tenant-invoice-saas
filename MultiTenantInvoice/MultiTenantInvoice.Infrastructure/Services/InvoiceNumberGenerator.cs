using Microsoft.EntityFrameworkCore;
using MultiTenantInvoice.Application.Common.Interfaces;
using MultiTenantInvoice.Domain.Entities;
using MultiTenantInvoice.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiTenantInvoice.Infrastructure.Services
{
    public class InvoiceNumberGenerator : IInvoiceNumberGenerator
    {
        private readonly AppDbContext _dbContext;
        private readonly ITenantProvider _tenantProvider;

        public InvoiceNumberGenerator(
            AppDbContext dbContext,
            ITenantProvider tenantProvider)
        {
            _dbContext = dbContext;
            _tenantProvider = tenantProvider;
        }

        public async Task<string> GenerateAsync(
            CancellationToken cancellationToken)
        {
            var tenantId = _tenantProvider.GetTenantId();

            var year = DateTime.UtcNow.Year;

            var sequence = await _dbContext.InvoiceNumberSequences
                .FirstOrDefaultAsync(
                    x => x.TenantId == tenantId && x.Year == year,
                    cancellationToken);

            if (sequence == null)
            {
                sequence = new InvoiceNumberSequence
                {
                    Id = Guid.NewGuid(),
                    TenantId = tenantId,
                    Year = year,
                    LastNumber = 0
                };

                _dbContext.InvoiceNumberSequences.Add(sequence);
            }

            sequence.LastNumber++;

            await _dbContext.SaveChangesAsync(cancellationToken);

            return $"INV-{year}-{sequence.LastNumber:00000}";
        }
    }
}
