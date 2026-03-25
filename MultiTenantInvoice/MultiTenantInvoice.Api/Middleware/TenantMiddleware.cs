using MultiTenantInvoice.Application.Common.Tenant;

namespace MultiTenantInvoice.Api.Middleware
{
    public class TenantMiddleware
    {
        private readonly RequestDelegate _next;

        public TenantMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, TenantContext tenantContext)
        {
            if (context.Request.Headers.TryGetValue("X-Tenant-Id", out var tenantId))
            {
                if (Guid.TryParse(tenantId, out var parsedTenant))
                {
                    tenantContext.TenantId = parsedTenant;
                }
            }

            await _next(context);
        }
    }
}
