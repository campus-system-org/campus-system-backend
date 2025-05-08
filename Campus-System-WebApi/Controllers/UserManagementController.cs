using Campus_System_WebApi.Controllers.Bases;
using Campus_System_WebApi.Services;
using Campus_System_WebApi.Services.Common;
using Microsoft.AspNetCore.Mvc;

namespace Campus_System_WebApi.Controllers
{
    /// <summary>
    /// 用戶管理
    /// </summary>
    [Route("user-management")]
    public class UserManagementController : AuthControllerBase
    {
        private readonly UserManagementService _service;

        public UserManagementController(UserManagementService service)
        {
            this._service = service;
        }

        [HttpPost("get-list")]
        public async Task<PagedResponseBase<UserManagementGetListResponse>> GetList([FromBody]UserManagementGetListRequest request)
        {
            UserManagementGetListResponse response = await _service.GetList(request);
            return new PagedResponseBase<UserManagementGetListResponse>(response);
        }
    }
}
