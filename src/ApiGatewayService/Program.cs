


using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);


builder.Configuration.AddJsonFile("ocelot.json");

builder.Services.AddOcelot();

var app = builder.Build();

app.UseOcelot().Wait();

app.Run();

// builder.Services.AddControllers();

// builder.Services.AddHttpClient();

// // Add services to the container.
// // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
// builder.Services.AddEndpointsApiExplorer();

// builder.Services.AddSwaggerGen();

// builder.Services.AddInfrastructure(builder.Configuration)
//         .AddInfrastructure(builder.Configuration);

// var app = builder.Build();

// // Configure the HTTP request pipeline.
// //if (app.Environment.IsDevelopment())
// //{
//     app.UseSwagger();
//     app.UseSwaggerUI();
// //}

// //app.UseHttpsRedirection();

// app.MapControllers();


// app.Run();



