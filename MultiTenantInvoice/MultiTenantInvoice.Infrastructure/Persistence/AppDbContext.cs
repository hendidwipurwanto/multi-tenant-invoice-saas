using MediatR;
using Microsoft.EntityFrameworkCore;
using MultiTenantInvoice.Application.Common.Interfaces;
using MultiTenantInvoice.Domain.Entities;
using MultiTenantInvoice.Infrastructure.Persistence.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiTenantInvoice.Infrastructure.Persistence
{
    public class AppDbContext: DbContext, IAppDbContext
    {
        private readonly ITenantProvider _tenantProvider;
        private readonly IMediator _mediator;

        public AppDbContext(DbContextOptions<AppDbContext> options,ITenantProvider tenantProvider,IMediator mediator) : base(options)
        {
            _tenantProvider = tenantProvider;
            _mediator = mediator;
        }

        public DbSet<MultiTenantInvoice.Domain.Entities.Tenant> Tenants { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<Customer> Customers { get; set; }

        public DbSet<Invoice> Invoices { get; set; }

        public DbSet<InvoiceNumberSequence> InvoiceNumberSequences { get; set; }

        public DbSet<InvoiceItem> InvoiceItems { get; set; }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var domainEntities = ChangeTracker
                .Entries<BaseEntity>()
                .Where(x => x.Entity.DomainEvents.Any())
                .ToList();

            var domainEvents = domainEntities
                .SelectMany(x => x.Entity.DomainEvents)
                .ToList();

            var result = await base.SaveChangesAsync(cancellationToken);

            foreach (var domainEvent in domainEvents)
            {
                await _mediator.Publish(domainEvent, cancellationToken);
            }

            foreach (var entity in domainEntities)
            {
                entity.Entity.ClearDomainEvents();
            }

            return result;
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);

            modelBuilder.ApplyGlobalFilters(_tenantProvider.GetTenantId());

        }
    }
}
