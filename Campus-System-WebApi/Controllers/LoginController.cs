using Campus_System_WebApi.Controllers.Bases;
using Campus_System_WebApi.Services;
using Campus_System_WebApi.Services.Common;
using Microsoft.AspNetCore.Mvc;

namespace Campus_System_WebApi.Controllers
{
    [Route("login")]
    [ApiExplorerSettings(GroupName = "General")]
    public class LoginController : NoAuthControllerBase
    {
        private readonly LoginService _service;

        public LoginController(LoginService service)
        {
            this._service = service;
        }

        /// <summary>
        /// 以 creator 登入
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("creator")]
        public async Task<ResponseBase<LoginCreatorResponse>> LoginCreator(LoginCreatorRequest request)
        {
            var response = await _service.LoginCreator(request);
            return new ResponseBase<LoginCreatorResponse>(response);
        }

        /// <summary>
        /// 以 admin 登入
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("admin")]
        public async Task<ResponseBase<LoginAdminResponse>> LoginAdmin(LoginAdminRequest request)
        {
            var response = await _service.LoginAdmin(request);
            return new ResponseBase<LoginAdminResponse>(response);
        }

        /// <summary>
        /// 以 manager 登入
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("manager")]
        public async Task<ResponseBase<LoginManagerResponse>> LoginManager(LoginManagerRequest request)
        {
            var response = await _service.LoginManager(request);
            return new ResponseBase<LoginManagerResponse>(response);
        }

        /// <summary>
        /// 以 teacher 登入
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("teacher")]
        public async Task<ResponseBase<LoginTeacherResponse>> LoginTeacher(LoginTeacherRequest request)
        {
            var response = await _service.LoginTeacher(request);
            return new ResponseBase<LoginTeacherResponse>(response);
        }

        /// <summary>
        /// 以 student 登入
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("student")]
        public async Task<ResponseBase<LoginStudentResponse>> LoginStudent(LoginStudentRequest request)
        {
            var response = await _service.LoginStudent(request);
            return new ResponseBase<LoginStudentResponse>(response);
        }
    }


}
