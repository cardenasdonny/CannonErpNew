using Api.Gateway.Web.Config;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Obtenemos la configuración (appsettings.json)
var configuration = builder.Configuration;

// Configuración de los CORS
//builder.Services.ConfigureCors();



builder.Services.AddAppsettingBinding(configuration)
                    .AddProxiesRegistration(configuration);

builder.Services.AddControllers();

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

if (app.Environment.IsDevelopment()) 
    app.UseDeveloperExceptionPage(); 
else app.UseHsts();

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseForwardedHeaders(new ForwardedHeadersOptions
{
    ForwardedHeaders = ForwardedHeaders.All
});

//app.UseCors("CorsPolicy");

app.UseAuthorization();

app.UseAuthentication();

app.MapControllers();

app.Run();
