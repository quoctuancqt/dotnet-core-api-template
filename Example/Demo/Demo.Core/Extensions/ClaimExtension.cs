using System.Linq;
using System.Security.Claims;
using System.Security.Principal;

namespace Demo.Core.Extensions
{
    public static class ClaimExtension
    {
        public static string GetValue(this IPrincipal user, string key)
        {
            if (user == null)
            {
                return string.Empty;
            }

            var claim = ((ClaimsIdentity)user.Identity).Claims.FirstOrDefault(x => x.Type.Equals(key));

            if (claim == null)
            {
                return string.Empty;
            }

            return claim.Value;
        }
    }

    public static class ClaimKey
    {
        public const string USER_ID = ClaimTypes.NameIdentifier;
    }
}
