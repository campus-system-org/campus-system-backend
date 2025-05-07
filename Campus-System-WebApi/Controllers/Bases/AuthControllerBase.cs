using Campus_System_WebApi.Swaggers;
using Microsoft.AspNetCore.Mvc;

namespace Campus_System_WebApi.Controllers.Bases
{
    /// <summary>
    /// 需驗證的路由
    /// </summary>
    [AuthButton]
    [ApiExplorerSettings(GroupName = "General")]
    public class AuthControllerBase
    {
    }
}
