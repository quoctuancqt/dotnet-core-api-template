using System.Threading.Tasks;
using Dto;
using JwtTokenServer.Proxies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ApiServer.Controllers
{
    public class AccountController : ApiBase
    {
        private readonly OAuthClient _oAuthClient;

        public AccountController(OAuthClient oAuthClient)
        {
            _oAuthClient = oAuthClient;
        }

        [HttpPost]
        [Route("login")]
        [AllowAnonymous]
        public async Task<IActionResult> LoginAsync([FromBody] LoginDto dto)
        {
            var response = await _oAuthClient.EnsureApiTokenAsync(dto.Username, dto.Password);

            if (response.Success) return Ok(response.Result);

            return BadRequest(response.Result);
        }

        [HttpGet("{name}")]
        public IActionResult Get(string name)
        {
            return Ok($"Hello {name}");
        }
    }
}
