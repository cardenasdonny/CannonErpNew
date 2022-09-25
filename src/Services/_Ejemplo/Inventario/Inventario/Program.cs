using HealthChecks.UI.Client;
using Inventario.Application.Queries;
using Inventario.Contracts.Articulo;
using Inventario.Entities.DbContexts;
using MediatR;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var configuration = builder.Configuration;

builder.Services.AddControllers();

builder.Services.AddDbContext<RepositoryContextFactory>(options =>
    options.UseSqlServer(configuration.GetConnectionString("sqlConnection"))
);

builder.Services.AddHealthChecks()
    .AddCheck("self", () => HealthCheckResult.Healthy())
    //.AddCheck("Inventario.Application", () => HealthCheckResult.Healthy())
    .AddDbContextCheck<RepositoryContextFactory>();

//builder.Services.AddHealthChecksUI();

builder.Services.AddMediatR(Assembly.Load("Inventario.Application"));

builder.Services.AddScoped<IArticuloQueryService, ArticuloQueryService>();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseAuthorization();

app.MapControllers();

app.MapHealthChecks("/healthz", new HealthCheckOptions()
{
    Predicate = _ => true,
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});

//app.MapHealthChecksUI();

app.Run();
