using Microsoft.EntityFrameworkCore;
using MultiTenantInvoice.Api.Middleware;
using MultiTenantInvoice.Application.Common.Interfaces;
using MultiTenantInvoice.Application.Common.Tenant;
using MultiTenantInvoice.Infrastructure.Persistence;
using MultiTenantInvoice.Infrastructure.Tenant;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IAppDbContext>(provider => provider.GetRequiredService<AppDbContext>());
// Add services to the container.
builder.Services.AddScoped<TenantContext>();
builder.Services.AddScoped<ITenantProvider, TenantProvider>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});


var app = builder.Build();

app.UseMiddleware<TenantMiddleware>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
