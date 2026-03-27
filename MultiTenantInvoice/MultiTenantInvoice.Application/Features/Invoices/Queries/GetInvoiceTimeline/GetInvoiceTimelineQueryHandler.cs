using MediatR;
using Microsoft.EntityFrameworkCore;
using MultiTenantInvoice.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiTenantInvoice.Application.Features.Invoices.Queries.GetInvoiceTimeline
{
    public class GetInvoiceTimelineQueryHandler
     : IRequestHandler<GetInvoiceTimelineQuery, List<InvoiceTimelineDto>>
    {
        private readonly IAppDbContext _dbContext;

        public GetInvoiceTimelineQueryHandler(IAppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<InvoiceTimelineDto>> Handle(
            GetInvoiceTimelineQuery request,
            CancellationToken cancellationToken)
        {
            var logs = await _dbContext.AuditLogs
                .Where(x => x.EntityName == "Invoice"
                         && x.EntityId == request.InvoiceId.ToString())
                .OrderBy(x => x.CreatedAt)
                .Select(x => new InvoiceTimelineDto
                {
                    EventType = x.EventType,
                    CreatedAt = x.CreatedAt
                })
                .ToListAsync(cancellationToken);

            return logs;
        }
    }
}
