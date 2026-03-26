using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiTenantInvoice.Application.Common.Interfaces
{
    public interface IPaymentProcessor
    {
        Task<bool> ProcessPayment(decimal amount);
    }
}
