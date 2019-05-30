using Microsoft.Extensions.DependencyInjection;

namespace CoreApiTemplate.Application
{
    public static class FluentValidationConfiguration
    {
        public static IServiceCollection RegisterValidations(this IServiceCollection services)
        {
            return services;
        }
    }
}
