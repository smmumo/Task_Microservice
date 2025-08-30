


using ApiGatewayService.Extensions;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);


builder.Configuration.AddJsonFile("RoutesConfig/ocelot.json");

// Bind JwtOptions from configuration
builder.Services.Configure<JwtSettingOptions>(builder.Configuration.GetSection("JwtSettings"));

builder.Services.AddJwtAuthentication(builder.Configuration);


builder.Services.AddOcelot();

var app = builder.Build();

app.UseOcelot().Wait();

app.UseAuthentication();

app.UseAuthorization();



app.Run();





