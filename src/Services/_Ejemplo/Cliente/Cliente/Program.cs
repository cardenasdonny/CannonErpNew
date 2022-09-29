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
using System.Security.Principal;
using Common.Logger;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var configuration = builder.Configuration;

var a = configuration.GetConnectionString("sqlConnection");

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
    .Enrich.WithProperty("Usuario", "System.Cannon")
    .WriteTo.Seq("http://srvtrial:5642/")
    .CreateLogger();

//HttpContextAccessor
builder.Services.AddHttpContextAccessor();

builder.Services.AddTransient<IPrincipal>(provider => provider.GetService<IHttpContextAccessor>().HttpContext.User);

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
    //Ejemplo del uso de los log:

    // En la clase ClienteCreateEventHandler del servicio Cliente hay un ejemplo de como obtener el usuario logueado, si no se envia en el log
    // se coloca por defecto System.Cannon
    var emailUsuario = "usuario.cannon";

    var obj = new { propiedad1 = "Propiedad 1", propiedad2 = "Propiedad 2" };
   
    Logs.Information("Título mensaje", "Esto es una información");
    Logs.Information("Título mensaje", "Esto es una información", emailUsuario);
    Logs.Information("Título mensaje", "Esto es una información", emailUsuario, obj);
    Logs.Warning("Título mensaje", "Esto es un warning", emailUsuario);
    Logs.Error("Título mensaje", "Esto es un error", emailUsuario);
    Logs.FatalError("Título mensaje", "Esto es un fatal error", emailUsuario); 
    Logs.Information("Título mensaje", "Esto es una información", emailUsuario, obj);

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
