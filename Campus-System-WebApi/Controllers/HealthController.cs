using Microsoft.AspNetCore.Mvc;

namespace Campus_System_WebApi.Controllers
{
    [ApiController]
    [Route("health")]
    public class HealthController : ControllerBase
    {

        [HttpGet()]
        public async Task<IActionResult> Health()
        {
            return Ok();
        }
    }
}
