using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiTenantInvoice.Domain.Entities
{
    public class AuditLog : BaseEntity
    {
        public string Action { get; set; }

        public string EntityName { get; set; }

        public string EntityId { get; set; }

        public string? Description { get; set; }

        public string? PerformedBy { get; set; }
    }
}
