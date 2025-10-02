using EdTech.Core.Interfaces.Repositories;
using EdTech.Infrastructure.Context;
using EdTech.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EdTech.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureDI(this IServiceCollection services, IConfiguration config)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseNpgsql(
                     GetConnectionString(config),
                     npgsqlOptions => npgsqlOptions
                     .MigrationsAssembly("EdTech.Infrastructure")
                ).UseSnakeCaseNamingConvention();
            });

            services.AddScoped(typeof(IReadOnlyRepository<,>), typeof(ReadOnlyRepository<,>));
            services.AddScoped(typeof(IWriteOnlyRepository<,>), typeof(WriteOnlyRepository<,>));
            services.AddScoped<IStudentRepository, StudentRepository>();

            return services;

        }

        private static string GetConnectionString(IConfiguration config)
        {
            var host = config["POSTGRES_SERVER"] ?? "localhost";
            var port = config["POSTGRES_PORT"] ?? "5432";
            var database = config["POSTGRES_DB"] ?? "edtechdb";
            var username = config["POSTGRES_USER"] ?? "postgres";
            var password = config["POSTGRES_PASSWORD"] ?? "postgres";
            return $"Host={host};Port={port};Database={database};Username={username};Password={password};";
        }
    }
}
