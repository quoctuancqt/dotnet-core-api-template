using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CoreApiTemplate.ApiServer.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public abstract class ApiBase : ControllerBase
    {
        protected OkObjectResult Success()
        {
            return Ok(new { success = true });
        }
    }
}
