using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiTenantInvoice.Domain.Common
{
    public abstract class BaseEntity : IMultiTenantEntity, ISoftDelete
    {
        public Guid Id { get; set; }

        public Guid TenantId { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }
    }
}
