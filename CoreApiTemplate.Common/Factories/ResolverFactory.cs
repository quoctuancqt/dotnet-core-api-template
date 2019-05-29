using Microsoft.AspNetCore.Http;
using System;
using System.Security.Claims;

namespace CoreApiTemplate.Common.Factories
{
    public static class ResolverFactory
    {
        private static IServiceProvider _serviceProvider { set; get; }

        private static IHttpContextAccessor _httpContextAccessor { get; set; }

        public static void SetProvider(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;

            _httpContextAccessor = GetService<IHttpContextAccessor>();
        }

        public static T GetService<T>()
           where T : class
        {
            return (T)_serviceProvider.GetService(typeof(T));
        }

        public static T CreateInstance<T>(string typeName)
            where T : class
        {

            Type type = Type.GetType(typeName);

            T instance = (T)Activator.CreateInstance(type);

            return instance;
        }

        public static T CreateInstance<T>(string typeName, params object[] args)
            where T : class
        {

            Type type = Type.GetType(typeName);

            T instance = (T)Activator.CreateInstance(type, args);

            return instance;
        }

        public static T GetPropValue<T>(this object src, string propName)
        {
            return (T)src.GetType().GetProperty(propName).GetValue(src, null);
        }

        public static string GetCurrentUserId()
        {
            return _httpContextAccessor?.HttpContext?.User.GetPropValue<string>(ClaimTypes.NameIdentifier);
        }
    }
}
