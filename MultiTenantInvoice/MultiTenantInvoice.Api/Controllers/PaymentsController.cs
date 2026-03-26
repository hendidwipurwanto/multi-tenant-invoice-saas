using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MultiTenantInvoice.Application.Features.Payments.Commands.AttemptPayment;
using MultiTenantInvoice.Application.Features.Payments.Queries.GetPaymentById;

namespace MultiTenantInvoice.Api.Controllers
{
    [ApiController]
    [Route("api/payments")]
    public class PaymentsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PaymentsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> AttemptPayment(
            [FromBody] AttemptPaymentCommand command)
        {
            var paymentId = await _mediator.Send(command);

            return Ok(paymentId);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPayment(Guid id)
        {
            var result = await _mediator.Send(
                new GetPaymentByIdQuery { PaymentId = id });

            return Ok(result);
        }
    }
}
