using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Moq;
using MultiTenantInvoice.Application.Common.Interfaces;
using MultiTenantInvoice.Application.Features.Payments.Commands.AttemptPayment;
using MultiTenantInvoice.Domain.Entities;
using MultiTenantInvoice.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiTenantInvoice.Application.Tests.Commands.AttemptPayment
{
    public class AttemptPaymentCommandHandlerTests
    {
        private readonly Mock<IAppDbContext> _dbContextMock;
        private readonly Mock<IPaymentProcessor> _paymentProcessorMock;

        public AttemptPaymentCommandHandlerTests()
        {
            _dbContextMock = new Mock<IAppDbContext>();
            _paymentProcessorMock = new Mock<IPaymentProcessor>();
        }

        [Fact]
        public async Task Handle_ShouldCreatePaymentAndReturnPaymentId()
        {
            // Arrange
            var command = new AttemptPaymentCommand
            {
                InvoiceId = Guid.NewGuid(),
                Amount = 100,
                Method = PaymentMethod.QRIS
            };

            var payments = new List<Payment>().AsQueryable();
            var invoices = new List<Invoice>
        {
            new Invoice { Id = command.InvoiceId }
        }.AsQueryable();

            var paymentsDbSet = new Mock<DbSet<Payment>>();
            paymentsDbSet.As<IQueryable<Payment>>().Setup(m => m.Provider).Returns(payments.Provider);
            paymentsDbSet.As<IQueryable<Payment>>().Setup(m => m.Expression).Returns(payments.Expression);
            paymentsDbSet.As<IQueryable<Payment>>().Setup(m => m.ElementType).Returns(payments.ElementType);
            paymentsDbSet.As<IQueryable<Payment>>().Setup(m => m.GetEnumerator()).Returns(payments.GetEnumerator());

            var invoicesDbSet = new Mock<DbSet<Invoice>>();
            invoicesDbSet.As<IQueryable<Invoice>>().Setup(m => m.Provider).Returns(invoices.Provider);
            invoicesDbSet.As<IQueryable<Invoice>>().Setup(m => m.Expression).Returns(invoices.Expression);
            invoicesDbSet.As<IQueryable<Invoice>>().Setup(m => m.ElementType).Returns(invoices.ElementType);
            invoicesDbSet.As<IQueryable<Invoice>>().Setup(m => m.GetEnumerator()).Returns(invoices.GetEnumerator());

            _dbContextMock.Setup(x => x.Payments).Returns(paymentsDbSet.Object);
            _dbContextMock.Setup(x => x.Invoices).Returns(invoicesDbSet.Object);

            _dbContextMock
                .Setup(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(1);

            _paymentProcessorMock
                .Setup(x => x.ProcessPayment(It.IsAny<decimal>()))
                .ReturnsAsync(true);

            var handler = new AttemptPaymentCommandHandler(
                _dbContextMock.Object,
                _paymentProcessorMock.Object);

            // Act
            var result = await handler.Handle(command, default);

            // Assert
            result.Should().NotBeEmpty();

            _dbContextMock.Verify(
                x => x.SaveChangesAsync(It.IsAny<CancellationToken>()),
                Times.Once);
        }
    }
}
