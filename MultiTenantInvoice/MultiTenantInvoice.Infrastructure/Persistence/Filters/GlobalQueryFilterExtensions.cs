using Microsoft.EntityFrameworkCore;
using MultiTenantInvoice.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MultiTenantInvoice.Infrastructure.Persistence.Filters
{
    public static class GlobalQueryFilterExtensions
    {
        public static void ApplyGlobalFilters(this ModelBuilder modelBuilder, Guid tenantId)
        {
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                var clrType = entityType.ClrType;

                if (typeof(IMultiTenantEntity).IsAssignableFrom(clrType))
                {
                    var parameter = Expression.Parameter(clrType, "e");

                    var tenantProperty = Expression.Property(
                        parameter,
                        nameof(IMultiTenantEntity.TenantId));

                    var tenantCondition = Expression.Equal(
                        tenantProperty,
                        Expression.Constant(tenantId));

                    Expression finalExpression = tenantCondition;

                    if (typeof(ISoftDelete).IsAssignableFrom(clrType))
                    {
                        var isDeletedProperty = Expression.Property(
                            parameter,
                            nameof(ISoftDelete.IsDeleted));

                        var notDeleted = Expression.Equal(
                            isDeletedProperty,
                            Expression.Constant(false));

                        finalExpression = Expression.AndAlso(
                            tenantCondition,
                            notDeleted);
                    }

                    var lambda = Expression.Lambda(
                        finalExpression,
                        parameter);

                    modelBuilder.Entity(clrType)
                        .HasQueryFilter(lambda);
                }
            }
        }
    }
}
