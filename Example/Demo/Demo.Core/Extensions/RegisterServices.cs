using AutoMapper;
using Demo.Common.Factories;
using Demo.Core.Interfaces;
using Demo.Core.Logging;
using Demo.Core.Repository;
using Demo.Core.Resilience;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Demo.Core.Extensions
{
    public static class RegisterServices
    {
        public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
        {
            ResolverFactory.SetProvider(services.BuildServiceProvider());

            services.AddSingleton(typeof(IAppLogger<>), typeof(LoggerAdapter<>));

            services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));

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

            return services;
        }
    }
}
