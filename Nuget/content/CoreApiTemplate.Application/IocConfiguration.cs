using Microsoft.Extensions.DependencyInjection;

namespace CoreApiTemplate.Application
{
    public static class IocConfiguration
    {
        public static IServiceCollection AddCustomServices(this IServiceCollection services)
        {
            // Register all custom services here

            return services;
        }
    }
}
