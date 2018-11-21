using System.Collections.Generic;
using System.Threading.Tasks;
using JwtTokenServer.Proxies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ApiServer.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly OAuthClient _oAuthClient;

        public AccountController(OAuthClient oAuthClient)
        {
            _oAuthClient = oAuthClient;
        }

        [HttpPost]
        [Route("login")]
        [AllowAnonymous]
        public async Task<IActionResult> LoginAsync([FromBody] IDictionary<string,string> dic)
        {
            var response = await _oAuthClient.EnsureApiTokenAsync(dic["UserName"], dic["Password"]);

            if (response.Success) return Ok(response.Result);

            return BadRequest(response.Result);
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await Task.FromResult("Call from authorized method"));
        }
    }
}
