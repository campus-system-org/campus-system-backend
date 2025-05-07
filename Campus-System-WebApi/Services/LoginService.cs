
using System.ComponentModel;
using System.Text.Json.Serialization;

namespace Campus_System_WebApi.Services
{
    public class LoginService
    {
        internal async Task<LoginCreatorResponse> LoginCreator(LoginCreatorRequest request)
        {
            throw new NotImplementedException();
        }

        internal async Task<LoginAdminResponse> LoginAdmin(LoginAdminRequest request)
        {
            throw new NotImplementedException();
        }

        internal async Task<LoginManagerResponse> LoginManager(LoginManagerRequest request)
        {
            throw new NotImplementedException();
        }

        internal async Task<LoginTeacherResponse> LoginTeacher(LoginTeacherRequest request)
        {
            throw new NotImplementedException();
        }

        internal async Task<LoginStudentResponse> LoginStudent(LoginStudentRequest request)
        {
            throw new NotImplementedException();
        }
    }

    public class LoginCreatorRequest
    {
        [JsonPropertyName("email")]
        [DefaultValue("email@example.com")]
        public string Email { get; set; }

        [JsonPropertyName("password")]
        [DefaultValue("password")]
        public string Password { get; set; }
    }

    public class LoginCreatorResponse
    {
        [JsonPropertyName("token")]
        public string Token { get; set; }
    }

    public class LoginAdminRequest
    {
        [JsonPropertyName("email")]
        [DefaultValue("email@example.com")]
        public string Email { get; set; }

        [JsonPropertyName("password")]
        [DefaultValue("password")]
        public string Password { get; set; }
    }

    public class LoginAdminResponse
    {
        [JsonPropertyName("token")]
        [DefaultValue("password")]
        public string Token { get; set; }
    }

    public class LoginManagerRequest
    {
        [JsonPropertyName("email")]
        [DefaultValue("email@example.com")]
        public string Email { get; set; }

        [JsonPropertyName("password")]
        [DefaultValue("password")]
        public string Password { get; set; }
    }

    public class LoginManagerResponse
    {
        [JsonPropertyName("token")]
        public string Token { get; set; }
    }

    public class LoginTeacherRequest
    {
        [JsonPropertyName("email")]
        [DefaultValue("email@example.com")]
        public string Email { get; set; }

        [JsonPropertyName("password")]
        [DefaultValue("password")]
        public string Password { get; set; }
    }

    public class LoginTeacherResponse
    {
        [JsonPropertyName("token")]
        [DefaultValue("password")]
        public string Token { get; set; }
    }

    public class LoginStudentRequest
    {
        [JsonPropertyName("email")]
        [DefaultValue("email@example.com")]
        public string Email { get; set; }

        [JsonPropertyName("password")]
        [DefaultValue("password")]
        public string Password { get; set; }
    }

    public class LoginStudentResponse
    {
        [JsonPropertyName("token")]
        public string Token { get; set; }
    }

}
