

using System.Text;
using Infrastructure;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Product.Api.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<JwtSettingOptions>(builder.Configuration.GetSection("JwtSettings"));

///builder.Services.AddJwtAuthentication(builder.Configuration);


builder.Services.AddControllers();

builder.Services.AddHttpClient();

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

//builder.Services.AddSwaggerGen();
builder.Services.AddSwaggerGen(options =>
    {
        options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
        {
            Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
            Name = "Authorization",
            In = ParameterLocation.Header,
            Type = SecuritySchemeType.Http,
            Scheme = "Bearer"
        });   
        options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    new string[] {}
                }
            });    
    });

builder.Services.AddInfrastructure(builder.Configuration)
        .AddInfrastructure(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.

    app.UseSwagger();
    
    app.UseSwaggerUI();
   

//app.UseAuthorization();

//app.UseHttpsRedirection();

app.MapControllers();

app.Run();



