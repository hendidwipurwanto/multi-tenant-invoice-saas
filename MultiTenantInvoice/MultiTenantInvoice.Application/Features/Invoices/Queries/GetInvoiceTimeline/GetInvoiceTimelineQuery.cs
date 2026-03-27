using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiTenantInvoice.Application.Features.Invoices.Queries.GetInvoiceTimeline
{
    public class GetInvoiceTimelineQuery : IRequest<List<InvoiceTimelineDto>>
    {
        public Guid InvoiceId { get; set; }
    }
}
