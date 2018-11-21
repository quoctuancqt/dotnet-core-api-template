using JwtTokenServer.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Scrutor;
using System;

namespace Core.Configurations
{
    public static class IocConfig
    {
        public static void AddServices(this IServiceCollection services)
        {
            services.AddHttpClient<Services.IProxyService, Services.ProxyService>();

            services.AddScoped(UnitOfWorkFactory);

            services.AddAccountManager<Services.AccountManager>();

            services.Scan(scan =>
                scan.FromCallingAssembly()
                    .AddClasses()
                    .UsingRegistrationStrategy(RegistrationStrategy.Skip)
                    .AsMatchingInterface()
                    .WithScopedLifetime());

            Core.Factories.ResolverFactory.SetProvider(services.BuildServiceProvider());
        }

        private static UnitOfWork.IUnitOfWork UnitOfWorkFactory(IServiceProvider serviceProvider)
        {
            object factory = serviceProvider.GetService<IHttpContextAccessor>();

            HttpContext context = ((HttpContextAccessor)factory).HttpContext;

            return new UnitOfWork.UnitOfWork(serviceProvider.GetService<Context>(), context);
        }
    }
}
