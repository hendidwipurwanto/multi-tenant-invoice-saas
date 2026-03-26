using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiTenantInvoice.Domain.Enums
{
    public enum PaymentMethod
    {
        BankTransfer = 0,
        QRIS = 1,
        CreditCard = 2
    }
}
