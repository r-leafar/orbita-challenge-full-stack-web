namespace EdTech.WebApi.Extensions
{
    public static class JsonSerializerExtension
    {
        public static IServiceCollection ConfigureJsonSerializerToPascalCase(this IServiceCollection services)
        {
            services.Configure<Microsoft.AspNetCore.Http.Json.JsonOptions>(options =>
            {
                options.SerializerOptions.PropertyNamingPolicy = null;
            });

            return services;
        }
    }
}
