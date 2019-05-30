using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Demo.ApiServer.Controllers
{
    [Authorize("AdminPolicy")]
    public class AdminController : ApiBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Ok("Call from admin controller");
        }
    }
}
