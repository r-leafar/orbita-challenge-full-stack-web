namespace EdTech.WebApi.Extensions
{
    public static class CorsExtensions
    {
        public static IServiceCollection AddFrontendCorsPolicy(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("AllowFrontend",
                    policy =>
                    {         policy.WithOrigins(configuration["FRONTEND_URL"] ?? "http://localhost:3000")
                              .AllowAnyHeader()
                              .AllowAnyMethod()
                              .AllowCredentials();
                    });
            });
            return services;
        }
    }
}
