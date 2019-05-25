using JwtTokenServer.Models;
using JwtTokenServer.Services;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Application.Services
{
    public class AccountManager : IAccountManager
    {
        public async Task<AccountResult> VerifyAccountAsync(string username, string password, TokenRequest tokenRequest)
        {
            await Task.CompletedTask;

            //Logic to check user is valid or not
            var user = new { Id = 1, FirstName = "Admin" };

            if (user == null) return new AccountResult(new { error = "Invalid user" });

            tokenRequest.Claims.Add(new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()));

            tokenRequest.Claims.Add(new Claim(ClaimTypes.Name, user.FirstName));

            tokenRequest.Responses.Add("userId", user.Id.ToString());

            return new AccountResult(tokenRequest);
        }
    }
}
