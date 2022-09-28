using Cliente.Entities.DbContexts;
using Microsoft.EntityFrameworkCore;
using MediatR;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using HealthChecks.UI.Client;
using Cliente.Contracts;
using Cliente.Application.Queries;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using Serilog.Events;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var configuration = builder.Configuration;

// Obtenemos el Environment true: Development false: Production
var env = builder.Environment.IsDevelopment() ? "Development" : "Production";


builder.Services.AddControllers();

// Log

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
    .Enrich.FromLogContext()
    //.WriteTo.Console()
    .Enrich.WithProperty("Servicio", typeof(Program).Assembly.GetName().Name)
    .Enrich.WithProperty("Environment", env)
    .WriteTo.Seq("http://localhost:5341/")
    .CreateLogger();

//HttpContextAccessor
builder.Services.AddHttpContextAccessor();

builder.Services.AddDbContext<RepositoryContextFactory>(options =>
    options.UseSqlServer(configuration.GetConnectionString("sqlConnection"))
);

// Health check
builder.Services.AddHealthChecks()
            .AddCheck("self", () => HealthCheckResult.Healthy())
            .AddDbContextCheck<RepositoryContextFactory>(typeof(RepositoryContextFactory).Name);

//builder.Services.AddHealthChecksUI();

// Event handlers
//builder.Services.AddMediatR(Assembly.Load("Cliente.Application"));
builder.Services.AddMediatR(typeof(Cliente.Application.AssemblyReference).Assembly);

// Query services
builder.Services.AddScoped<IClienteQueryService, ClienteQueryService>();

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


var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseAuthorization();
app.UseAuthentication();

app.MapControllers();
app.MapHealthChecks("/healthz", new HealthCheckOptions()
{
    Predicate = _ => true,
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});
try
{
    Log.Information("Iniciando Servicio Clientes {Servicio}", "Clientes");

    Log.Information("Información 1 {Servicio1}", "Servicio Cliente");


    /*
    Log.Warning("Mensaje de Warning en Program.cs - Warning servicio Cliente");
    
    Log.Error("Mensaje de excepción en la clase: Program.cs - Error al inicio del Servicio Clientes");
    Log.Fatal("Mensaje de excepción en la clase: Program.cs - Error Fatal al inicio del Servicio Clientes");

    Log.Information("Información 1 {Servicio}", "Servicio Cliente");
    Log.Information("Información de inventario {Servicio} {prpp}", "Inventario","","");
    */

    app.Run();
    

}
catch (Exception ex)
{
    Log.Fatal(ex, "Error al iniciar el servicio Clientes");

    return;
}
finally
{
    Log.CloseAndFlush();
}
