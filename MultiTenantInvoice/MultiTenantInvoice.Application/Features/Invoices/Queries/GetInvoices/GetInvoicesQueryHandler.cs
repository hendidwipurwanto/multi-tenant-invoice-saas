using MediatR;
using Microsoft.EntityFrameworkCore;
using MultiTenantInvoice.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiTenantInvoice.Application.Features.Invoices.Queries.GetInvoices
{
    public class GetInvoicesQueryHandler
    : IRequestHandler<GetInvoicesQuery, List<InvoiceListDto>>
    {
        private readonly IAppDbContext _dbContext;

        public GetInvoicesQueryHandler(IAppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<InvoiceListDto>> Handle(
            GetInvoicesQuery request,
            CancellationToken cancellationToken)
        {
            var query = _dbContext.Invoices.AsQueryable();

            if (!string.IsNullOrWhiteSpace(request.Search))
            {
                query = query.Where(x =>
                    x.InvoiceNumber.Contains(request.Search));
            }

            query = query
                .OrderByDescending(x => x.CreatedAt)
                .Skip((request.Page - 1) * request.PageSize)
                .Take(request.PageSize);

            var result = await query
                .Select(x => new InvoiceListDto
                {
                    Id = x.Id,
                    InvoiceNumber = x.InvoiceNumber,
                    Total = x.Total,
                    Status = x.Status.ToString(),
                    IssueDate = x.IssueDate
                })
                .ToListAsync(cancellationToken);

            return result;
        }
    }
}
