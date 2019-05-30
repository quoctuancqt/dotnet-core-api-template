using Demo.Domain.Identities;
using JwtTokenServer.Models;
using JwtTokenServer.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Demo.Application.Services
{
    public class AccountManager : IAccountManager
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IPasswordHasher<ApplicationUser> _passwordHasher;

        public AccountManager(UserManager<ApplicationUser> userManager,
            IPasswordHasher<ApplicationUser> passwordHasher)
        {
            _userManager = userManager;
            _passwordHasher = passwordHasher;
        }

        public async Task<AccountResult> VerifyAccountAsync(string username, string password, TokenRequest tokenRequest)
        {
            var user = await _userManager.FindByEmailAsync(username);

            if (user == null) return new AccountResult(new { error = "User is not correct." });

            if (_passwordHasher.VerifyHashedPassword(user, user.PasswordHash, password) == PasswordVerificationResult.Failed) return new AccountResult(new { error = "Password is not correct." }); ;

            var userRoles = await _userManager.GetRolesAsync(user);

            tokenRequest.Claims.Add(new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()));

            tokenRequest.Claims.Add(new Claim(ClaimTypes.Name, user.UserName));

            foreach (var role in userRoles)
            {
                tokenRequest.Claims.Add(new Claim(ClaimTypes.Role, role));
            }

            tokenRequest.Responses.Add("userId", user.Id.ToString());

            return new AccountResult(tokenRequest);
        }
    }
}
