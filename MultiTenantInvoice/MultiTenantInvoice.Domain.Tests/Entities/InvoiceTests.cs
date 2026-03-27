using FluentAssertions;
using MultiTenantInvoice.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiTenantInvoice.Domain.Tests.Entities
{
    public class InvoiceTests
    {
        [Fact]
        public void CalculateTotals_ShouldCalculateSubtotalTaxAndTotal()
        {
            // Arrange
            var invoice = new Invoice();

            invoice.Items.Add(new InvoiceItem
            {
                Quantity = 2,
                UnitPrice = 100
            });

            invoice.Items.Add(new InvoiceItem
            {
                Quantity = 1,
                UnitPrice = 50
            });

            // Act
            invoice.CalculateTotals();

            // Assert
            invoice.Subtotal.Should().Be(250);
            invoice.Tax.Should().Be(27.5m);
            invoice.Total.Should().Be(277.5m);
        }
    }
}
