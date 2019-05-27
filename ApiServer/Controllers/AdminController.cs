using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ApiServer.Controllers
{
    //[Authorize("AdminPolicy")]
    public class AdminController : ApiBase
    {
        public IActionResult Get()
        {
            return Ok("Call from admin controller!");
        }
    }
}
