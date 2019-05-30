using Demo.Core.Policies;
using Microsoft.AspNetCore.Authorization;

namespace Demo.Application.Policies
{
    public class AdminAuthorize : BaseAuthorize<AdminAuthorize>
    {
        protected override bool IsAuthorize(AuthorizationHandlerContext context, AdminAuthorize requirement)
        {
            return context.User.IsInRole("Admin");
        }
    }
}
