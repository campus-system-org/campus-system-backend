using Campus_System_WebApi.Controllers.Bases;
using Microsoft.AspNetCore.Mvc;

namespace Campus_System_WebApi.Controllers
{
    /// <summary>
    /// 開發者專用
    /// </summary>
    [ApiExplorerSettings(GroupName = "Developer")]
    [Route("developer")]
    public class DeveloperController : NoAuthControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> Test()
        {
            return Ok();
        }
    }
}
