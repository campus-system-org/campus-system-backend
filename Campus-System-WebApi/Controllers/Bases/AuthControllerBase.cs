using Campus_System_WebApi.Controllers.Middlewares;
using Campus_System_WebApi.Controllers.Pipelines;
using Campus_System_WebApi.Processors;
using Campus_System_WebApi.Swaggers;
using Microsoft.AspNetCore.Mvc;

namespace Campus_System_WebApi.Controllers.Bases
{
    /// <summary>
    /// 需驗證的路由
    /// </summary>
    [AuthButton]
    [ApiController]
    [ApiExplorerSettings(GroupName = "General")]
    [MiddlewareFilter(typeof(AuthPipeline))]
    public class AuthControllerBase : ControllerBase
    {
        /// <summary>
        /// 通過驗證的用戶資訊
        /// </summary>
        protected UserModel _CurrentUser => (HttpContext.Items[AuthMiddleware.USER_Model_KEY] as UserModel)!;
    }
}
