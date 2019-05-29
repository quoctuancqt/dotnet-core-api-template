using AutoMapper;
using CoreApiTemplate.Common.Factories;
using CoreApiTemplate.Core.Interfaces;
using CoreApiTemplate.Core.Logging;
using CoreApiTemplate.Core.Resilience;
using CoreApiTemplate.Dto;
using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace CoreApiTemplate.Core.Extensions
{
    public static class RegisterServices
    {
        public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
        {
            ResolverFactory.SetProvider(services.BuildServiceProvider());

            services.AddSingleton(typeof(IAppLogger<>), typeof(LoggerAdapter<>));

            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            if (configuration.GetValue<string>("UseResilientHttp") == bool.TrueString)
            {
                services.AddTransient<IResilientHttpClientFactory, ResilientHttpClientFactory>();
                services.AddTransient<IHttpClient, ResilientHttpClient>(sp => sp.GetService<IResilientHttpClientFactory>().CreateResilientHttpClient());
            }
            else
            {
                services.AddTransient<IHttpClient, StandardHttpClient>();
            }

            return services.RegisterValidators();
        }

        public static IServiceCollection RegisterValidators(this IServiceCollection services)
        {
            services.AddSingleton<IValidator<LoginDto>, LoginDtoValidator>();

            return services;
        }
    }
}
