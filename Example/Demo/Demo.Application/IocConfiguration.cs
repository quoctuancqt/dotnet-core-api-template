using Demo.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Demo.Application
{
    public static class IocConfiguration
    {
        public static IServiceCollection AddCustomServices(this IServiceCollection services)
        {
            // Register all custom services here
            services.AddScoped<ICategoryService, CategoryService>();

            return services;
        }
    }
}
