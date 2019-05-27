using Microsoft.AspNetCore.Authorization;

namespace Core.Policies
{
    public class ApiAuthorize : BaseAuthorize<ApiAuthorize>, IAuthorizationRequirement
    {
        protected override bool IsAuthorize(AuthorizationHandlerContext context, ApiAuthorize requirement)
        {
            return true;
        }
    }
}
