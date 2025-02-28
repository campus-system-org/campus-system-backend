using Campus_System_WebApi.Exceptions;
using Campus_System_WebApi.Services.Common;
using System.Text.Json;

namespace Campus_System_WebApi.Controllers.Middlewares
{
    public class GlobalExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public GlobalExceptionMiddleware(RequestDelegate _next)
        {
            this._next = _next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (CustomException ex)
            {
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = ex.ErrorCode;

                ResponseBase response = new ResponseBase
                {
                    Msg = ex.Message
                };

                await context.Response.WriteAsync(JsonSerializer.Serialize(response));
            }
            catch (Exception ex)
            {
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;

                ResponseBase response = new ResponseBase
                {
                    Msg = ex.Message
                };

                await context.Response.WriteAsync(JsonSerializer.Serialize(response));
            }
        }
    }
}
