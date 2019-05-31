using Demo.Application.Repositories;
using Demo.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Demo.Application
{
    public static class IocConfiguration
    {
        public static IServiceCollection AddCustomServices(this IServiceCollection services)
        {
            // Register all custom services here
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<ICategoryService, CategoryService>();

            return services;
        }
    }
}
