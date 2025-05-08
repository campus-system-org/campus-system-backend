using Campus_System_WebApi.Controllers.Bases;
using Campus_System_WebApi.Services;
using Campus_System_WebApi.Services.Common;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

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

        /// <summary>
        /// 取得使用者清單
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("get-list")]
        public async Task<PagedResponseBase<UserManagementGetListResponse>> GetList(UserManagementGetListRequest request)
        {
            UserManagementGetListResponse response = await _service.GetList(request);
            return new PagedResponseBase<UserManagementGetListResponse>(response);
        }

        /// <summary>
        /// 建立使用者
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("create-one")]
        [SwaggerResponse(StatusCodes.Status409Conflict, "此用戶已存在")]
        public async Task<ResponseBase> CreateOne(UserManagementCreateOneRequest request)
        {
            await _service.CreateOne(request);
            return new ResponseBase();
        }
    }
}
