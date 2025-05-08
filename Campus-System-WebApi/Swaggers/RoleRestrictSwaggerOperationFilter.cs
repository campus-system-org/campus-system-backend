using Campus_System_WebApi.ActionFilters;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Reflection;

namespace Campus_System_WebApi.Swaggers
{
    /// <summary>
    /// 在swagger上動態顯示 RoleRestrictAttributeAttribute 中允許的roles
    /// </summary>
    public class RoleRestrictSwaggerOperationFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            // 檢查是否有 BsAuthAttribute
            var bsAuthAttr = context.MethodInfo.GetCustomAttribute<RoleRestrictAttribute>();

            if (bsAuthAttr != null)
            {
                var roles = string.Join(", ", bsAuthAttr.AllowedRoles);
                var description = $"🚫 限定角色：{roles}";

                if (operation.Description == null)
                {
                    operation.Description = description;
                }
                else
                {
                    operation.Description = description + "\n\n\n\n" + operation.Description;
                }
            }
        }
    }
}
