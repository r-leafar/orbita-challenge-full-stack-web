using EdTech.Infrastructure;
using EdTech.Infrastructure.Context;
using EdTech.WebApi.Endpoints;
using EdTech.WebApi.Extensions;
using EdTech.WebApi.Middlewares;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration;

builder.Services.AddInfrastructureDI(configuration);
builder.Services.AddFrontendCorsPolicy(configuration);
builder.Services.ConfigureJsonSerializerToPascalCase();


var app = builder.Build();

app.UseCors("AllowFrontend");

app.UseMiddleware<ErrorHandlingMiddleware>();

app.UseHttpsRedirection();

app.MapGroup("/api/v1/students").MapStudentEndpoints();

// Executa migrations automaticamente no startup
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    db.Database.Migrate();
}

app.Run();

