using Auth.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// HttpContextAccessor
builder.Services.AddHttpContextAccessor();


// Identity
builder.Services.AddJWTTokenServices(builder.Configuration);



builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseAuthorization();

app.MapControllers();

app.Run();
