using Microsoft.Extensions.DependencyInjection;
using System;

namespace Core.Extensions
{
    public static class HttpClientExtension
    {
        public static IServiceCollection AddHttpClient<TClient>(this IServiceCollection services, Uri baseAddress)
            where TClient : class
        {
            services.AddHttpClient<TClient>(typeof(TClient).Name, client => client.BaseAddress = baseAddress);

            return services;
        }
    }
}
