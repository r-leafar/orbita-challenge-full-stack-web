using EdTech.Infrastructure;
using EdTech.WebApi.Endpoints;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddInfrastructureDI(builder.Configuration);

// Add services to the container.

var app = builder.Build();

app.UseHttpsRedirection();

app.MapGroup("/api/v1/students").MapStudentEndpoints();

app.Run();

