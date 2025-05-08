using Campus_System_WebApi.Controllers.Middlewares;

namespace Campus_System_WebApi.Controllers.Pipelines
{
    /// <summary>
    /// 驗證用管道
    /// </summary>
    public class AuthPipeline
    {
        public void Configure(IApplicationBuilder app)
        {
            app.UseMiddleware<AuthMiddleware>();
        }
    }
}
