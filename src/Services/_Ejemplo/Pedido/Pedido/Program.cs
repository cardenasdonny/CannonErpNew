using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.IdentityModel.Tokens;
using Pedido.Application.Queries;
using Pedido.Contracts;
using Pedido.Entities.DbContexts;
using Pedido.Proxies;
using Pedido.Proxies.Inventario;
using System.Reflection;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var configuration = builder.Configuration;

// HttpContextAccessor
builder.Services.AddHttpContextAccessor();

builder.Services.AddDbContext<RepositoryContextFactory>(options =>
    options.UseSqlServer(configuration.GetConnectionString("sqlConnection"))
);

builder.Services.AddHealthChecks()
    .AddCheck("self", () => HealthCheckResult.Healthy())
    //.AddCheck("Inventario.Application", () => HealthCheckResult.Healthy())
    .AddDbContextCheck<RepositoryContextFactory>();

//builder.Services.AddHealthChecksUI();

// Event handlers
builder.Services.AddMediatR(Assembly.Load("Pedido.Application"));

builder.Services.AddScoped<IPedidoQueryService, PedidoQueryService>();

// ApiUrls
builder.Services.Configure<ApiUrls>(opts => configuration.GetSection("ApiUrls").Bind(opts));

// Proxies
builder.Services.AddHttpClient<IInventarioProxy, InventarioProxy>(); // para que tambien inyecte el httplient

// Add Authentication

var secretKey = Encoding.ASCII.GetBytes(
    configuration.GetValue<string>("SecretKey")
);

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(x =>
{
    x.RequireHttpsMetadata = false;
    x.SaveToken = true;
    x.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(secretKey),
        ValidateIssuer = false,
        ValidateAudience = false
    };
});

builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseAuthorization();

app.MapControllers();

app.Run();
