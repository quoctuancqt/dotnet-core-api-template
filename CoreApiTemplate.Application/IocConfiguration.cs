using Microsoft.Extensions.DependencyInjection;

namespace CoreApiTemplate.Application
{
    public static class IocConfiguration
    {
        public static IServiceCollection AddCustomServices(this IServiceCollection services)
        {
            services.Scan(scan =>
            {
                scan.FromCallingAssembly()
                        .AddClasses()
                        .AsMatchingInterface();
            });

            return services;
        }
    }
}
