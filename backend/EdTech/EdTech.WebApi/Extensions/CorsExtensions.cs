namespace EdTech.WebApi.Extensions
{
    public static class CorsExtensions
    {
        public static IServiceCollection AddFrontendCorsPolicy(this IServiceCollection services, IConfiguration configuration)
        {
            var clientBaseUrl = configuration["VITE_CLIENT_URL"] ?? "http://localhost";
            var clientPort = configuration["VITE_PORT"] ?? "3000";
            var clientUrl = $"{clientBaseUrl}:{clientPort}";

            services.AddCors(options =>
            {
                options.AddPolicy("AllowFrontend",
                    policy =>
                    {         policy.WithOrigins(clientUrl)
                              .AllowAnyHeader()
                              .AllowAnyMethod()
                              .AllowCredentials();
                    });
            });
            return services;
        }
    }
}
