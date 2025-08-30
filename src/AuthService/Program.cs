


using AuthService.Contracts;
using AuthService.Extensions;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddHttpClient();

builder.Services.AuthDependencyInjection(builder.Configuration);

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Information()
    .WriteTo.Console()
    .WriteTo.File("logs/log-.txt", rollingInterval: RollingInterval.Day)
    .Enrich.FromLogContext()   
    .CreateLogger();

//builder.Host.UseSerilog();

// Bind JwtOptions from configuration
builder.Services.Configure<JwtSettingOptions>(builder.Configuration.GetSection("JwtSettings"));

builder.Services.AddJwtAuthentication(builder.Configuration);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

builder.Services.AddAuthorization();

var app = builder.Build();


// Configure the HTTP request pipeline.
// //if (app.Environment.IsDevelopment())
// //{
    app.UseSwagger();
    app.UseSwaggerUI();
// //}

// Add authentication and authorization middleware to the pipeline
app.UseAuthentication();

app.UseAuthorization();

//app.UseHttpsRedirection();

app.MapControllers();

app.Run();



