using FluentAssertions;
using Moq;
using MultiTenantInvoice.Application.Common.Interfaces;
using MultiTenantInvoice.Application.Features.Payments.Webhooks;
using MultiTenantInvoice.Domain.Entities;
using MultiTenantInvoice.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiTenantInvoice.Application.Tests.Webhooks
{
    public class HandlePaymentWebhookCommandHandlerTests
    {
        private readonly Mock<IAppDbContext> _dbContextMock;

        public HandlePaymentWebhookCommandHandlerTests()
        {
            _dbContextMock = new Mock<IAppDbContext>();
        }

        [Fact]
        public async Task Handle_ShouldUpdatePaymentStatusToSuccess()
        {
            // Arrange
            var paymentId = Guid.NewGuid();
            var invoiceId = Guid.NewGuid();

            var payment = new Payment
            {
                Id = paymentId,
                InvoiceId = invoiceId,
                Amount = 100,
                Status = PaymentStatus.Pending
            };

            var invoice = new Invoice
            {
                Id = invoiceId,
                Status = InvoiceStatus.Sent
            };

            var payments = new List<Payment> { payment }.AsQueryable();
            var invoices = new List<Invoice> { invoice }.AsQueryable();

            _dbContextMock.Setup(x => x.Payments.FindAsync(paymentId))
                .ReturnsAsync(payment);

            _dbContextMock.Setup(x => x.Invoices.FindAsync(invoiceId))
                .ReturnsAsync(invoice);

            var handler = new HandlePaymentWebhookCommandHandler(_dbContextMock.Object);

            var command = new HandlePaymentWebhookCommand
            {
                EventType = "payment.succeeded",
                PaymentId = paymentId,
                InvoiceId = invoiceId
            };

            // Act
            await handler.Handle(command, default);

            // Assert
            payment.Status.Should().Be(PaymentStatus.Success);
            invoice.Status.Should().Be(InvoiceStatus.Paid);
        }
    }
}
