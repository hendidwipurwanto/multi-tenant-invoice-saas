using MultiTenantInvoice.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiTenantInvoice.Infrastructure.Services
{
    public class FakePaymentProcessor : IPaymentProcessor
    {
        public Task<bool> ProcessPayment(decimal amount)
        {
            var random = new Random();

            var success = random.Next(1, 100) <= 70;

            return Task.FromResult(success);
        }
    }
}
