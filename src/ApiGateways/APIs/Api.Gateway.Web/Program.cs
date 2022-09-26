using Api.Gateway.Web.Config;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var configuration = builder.Configuration;

builder.Services.AddAppsettingBinding(configuration)
                    .AddProxiesRegistration(configuration);

builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseAuthorization();

app.UseAuthentication();

app.MapControllers();

app.Run();
