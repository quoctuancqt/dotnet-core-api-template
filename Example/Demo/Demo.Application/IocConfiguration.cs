using Demo.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Demo.Application
{
    public static class IocConfiguration
    {
        public static IServiceCollection AddCustomServices(this IServiceCollection services)
        {
            services.AddScoped<IProductService, ProductService>();

            return services;
        }
    }
}
