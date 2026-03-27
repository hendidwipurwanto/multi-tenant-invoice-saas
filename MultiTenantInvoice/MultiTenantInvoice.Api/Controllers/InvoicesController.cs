using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MultiTenantInvoice.Application.Features.Invoices.Queries.GetInvoiceTimeline;

namespace MultiTenantInvoice.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvoicesController : ControllerBase
    {
        private readonly IMediator _mediator;
        public InvoicesController(IMediator mediator    )
        {
            _mediator = mediator;
        }

        [HttpGet("{id}/timeline")]
        public async Task<IActionResult> GetTimeline(Guid id)
        {
            var result = await _mediator.Send(
                new GetInvoiceTimelineQuery { InvoiceId = id });

            return Ok(result);
        }
    }
}
