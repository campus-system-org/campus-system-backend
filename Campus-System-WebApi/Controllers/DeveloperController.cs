using Campus_System_WebApi.Controllers.Bases;
using Campus_System_WebApi.Services;
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
        private readonly DeveloperService _service;

        public DeveloperController(DeveloperService service)
        {
            this._service = service;
        }

        /// <summary>
        /// 建立任意權限用戶
        /// </summary>
        /// <returns></returns>
        [HttpPost("create-users")]
        public async Task<IActionResult> CreateUser(DeveloperCreateUserRequest request)
        {
            await _service.CreateUser(request);
            return Ok();
        }
    }
}
