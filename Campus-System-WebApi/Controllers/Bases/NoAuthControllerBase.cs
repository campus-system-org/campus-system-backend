using Microsoft.AspNetCore.Mvc;

namespace Campus_System_WebApi.Controllers.Bases
{
    /// <summary>
    /// 不需驗證的路由
    /// </summary>
    [ApiController]
    public abstract class NoAuthControllerBase : ControllerBase
    {
    }
}
