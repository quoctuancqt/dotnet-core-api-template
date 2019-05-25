using AutoMapper;
using Common.Factories;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Scrutor;
using System.Reflection;

namespace Core.Extensions
{
    public static class RegisterServices
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            ResolverFactory.SetProvider(services.BuildServiceProvider());

            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            services.Scan(scan =>
                scan.FromCallingAssembly()
                    .AddClasses()
                    .UsingRegistrationStrategy(RegistrationStrategy.Skip)
                    .AsMatchingInterface()
                    .WithScopedLifetime());

            return services.RegisterValidators();
        }

        public static IServiceCollection RegisterValidators(this IServiceCollection services)
        {
            //services.AddSingleton<IValidator<TDTO>, TDTOValidator>();

            return services;
        }
    }
}
