using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Campus_System_WebApi.Swaggers
{
    /// <summary>
    /// 如果 controller 帶有 AuthButtonAttribute，就顯示驗證按鈕
    /// </summary>
    public class AuthorizeCheckOperationFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            var hasAuthorize = context.MethodInfo.DeclaringType!.GetCustomAttributes(true)
                .OfType<AuthButtonAttribute>().Any() ||
                context.MethodInfo.GetCustomAttributes(true).OfType<AuthButtonAttribute>().Any();

            if (hasAuthorize)
            {
                operation.Security = new List<OpenApiSecurityRequirement>
            {
                new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Campus-System"
                            }
                        },
                        new List<string>()
                    }
                }
            };
            }
        }
    }
}
