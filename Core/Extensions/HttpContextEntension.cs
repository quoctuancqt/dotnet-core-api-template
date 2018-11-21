using Microsoft.AspNetCore.Http;

namespace Core.Extensions
{
    public static class HttpContextEntension
    {
        private static IHttpContextAccessor _accessor;

        public static void Configure(IHttpContextAccessor httpContextAccessor)
        {
            _accessor = httpContextAccessor;
        }

        public static HttpContext HttpContext => _accessor.HttpContext;
    }
}
