using CoreApiTemplate.Dto;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace CoreApiTemplate.Application
{
    public static class FluentValidationConfiguration
    {
        public static IServiceCollection RegisterValidations(this IServiceCollection services)
        {
            services.AddSingleton<IValidator<LoginDto>, LoginDtoValidator>();

            return services;
        }
    }
}
