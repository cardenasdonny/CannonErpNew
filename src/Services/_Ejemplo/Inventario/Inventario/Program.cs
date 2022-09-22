using Inventario.Application.Queries;
using Inventario.Contracts.Articulo;
using Inventario.Entities.DbContexts;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var configuration = builder.Configuration;

builder.Services.AddControllers();

builder.Services.AddDbContext<RepositoryContextFactory>(options =>
    options.UseSqlServer(configuration.GetConnectionString("sqlConnection"))
);

builder.Services.AddScoped<IArticuloQueryService, ArticuloQueryService>();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseAuthorization();

app.MapControllers();

app.Run();
