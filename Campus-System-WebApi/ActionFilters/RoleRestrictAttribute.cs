using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using Campus_System_Database_Model.Data;
using Campus_System_WebApi.Controllers.Middlewares;
using Campus_System_WebApi.Processors;

namespace Campus_System_WebApi.ActionFilters
{
    public class RoleRestrictAttribute : Attribute, IAsyncActionFilter
    {
        public UserRole[] AllowedRoles { get; }

        public RoleRestrictAttribute(params UserRole[] roles)
        {
            AllowedRoles = roles;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var user = context.HttpContext.Items[AuthMiddleware.USER_Model_KEY] as UserModel;

            if (user == null)
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            if (!AllowedRoles.Contains(user.Role))
            {
                throw new CustomException($"權限不足。限定: [{string.Join(",", AllowedRoles)}]", 403);
            }

            await next();
        }
    }
}
