using MediatR;
using MultiTenantInvoice.Application.Features.Invoices.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiTenantInvoice.Application.Features.Invoices.Commands
{
    public class CreateInvoiceCommand : IRequest<Guid>
    {
        public CreateInvoiceRequest Request { get; set; }
    }
}
