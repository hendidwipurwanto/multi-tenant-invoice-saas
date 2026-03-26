using FluentValidation;
using MultiTenantInvoice.Application.Features.Invoices.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiTenantInvoice.Application.Features.Invoices.Validators
{
    public class CreateInvoiceValidator : AbstractValidator<CreateInvoiceRequest>
    {
        public CreateInvoiceValidator()
        {
            RuleFor(x => x.CustomerId)
                .NotEmpty();

            RuleFor(x => x.Items)
                .NotEmpty();

            RuleForEach(x => x.Items).ChildRules(item =>
            {
                item.RuleFor(x => x.Description)
                    .NotEmpty()
                    .MaximumLength(200);

                item.RuleFor(x => x.Quantity)
                    .GreaterThan(0);

                item.RuleFor(x => x.UnitPrice)
                    .GreaterThan(0);
            });

            RuleFor(x => x.DueDate)
                .GreaterThan(x => x.IssueDate);
        }
    }
}
