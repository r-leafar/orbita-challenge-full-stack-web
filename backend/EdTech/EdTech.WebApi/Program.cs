using EdTech.Infrastructure;
using EdTech.Infrastructure.Context;
using EdTech.WebApi.Endpoints;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddInfrastructureDI(builder.Configuration);

// Add services to the container.

var app = builder.Build();

app.UseHttpsRedirection();

app.MapGroup("/api/v1/students").MapStudentEndpoints();

// Executa migrations automaticamente no startup
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    db.Database.Migrate();
}

app.Run();

