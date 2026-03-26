using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MultiTenantInvoice.Application.Features.Payments.Webhooks;

namespace MultiTenantInvoice.Api.Controllers
{
    [ApiController]
    [Route("api/webhooks/payments")]
    public class PaymentWebhookController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PaymentWebhookController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> HandleWebhook(
            [FromBody] PaymentWebhookDto dto)
        {
            var command = new HandlePaymentWebhookCommand
            {
                EventType = dto.EventType,
                PaymentId = dto.PaymentId,
                InvoiceId = dto.InvoiceId
            };

            await _mediator.Send(command);

            return Ok();
        }
    }
}
