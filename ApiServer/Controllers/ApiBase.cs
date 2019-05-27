using Microsoft.AspNetCore.Mvc;

namespace ApiServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public abstract class ApiBase : ControllerBase
    {
        protected OkObjectResult Success()
        {
            return Ok(new { success = true });
        }
    }
}
