using MediatR;
using Microsoft.EntityFrameworkCore;
using MultiTenantInvoice.Application.Common.Interfaces;
using MultiTenantInvoice.Application.Features.AuditLogs.Queries;
using MultiTenantInvoice.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiTenantInvoice.Application.Features.AuditLogs.EventHandlers
{
    public class GetEntityTimelineQueryHandler
    : IRequestHandler<GetEntityTimelineQuery, List<AuditLog>>
    {
        private readonly IAppDbContext _dbContext;

        public GetEntityTimelineQueryHandler(IAppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<AuditLog>> Handle(
            GetEntityTimelineQuery request,
            CancellationToken cancellationToken)
        {
            return await _dbContext.AuditLogs
                .Where(x =>
                    x.EntityName == request.EntityName &&
                    x.EntityId == request.EntityId)
                .OrderByDescending(x => x.CreatedAt)
                .ToListAsync(cancellationToken);
        }
    }
}
