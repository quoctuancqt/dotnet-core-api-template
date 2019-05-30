using Demo.Dto;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace Demo.Application
{
    public static class FluentValidationConfiguration
    {
        public static IServiceCollection RegisterValidations(this IServiceCollection services)
        {
            services.AddSingleton<IValidator<LoginDto>, LoginDtoValidator>();

            services.AddSingleton<IValidator<CreateCategoryDto>, CreateCategoryDtoValidator>();

            return services;
        }
    }
}
