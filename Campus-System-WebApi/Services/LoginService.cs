
using Campus_System_Database_Model.Data;
using Campus_System_WebApi.Processors;
using MongoGogo.Connection;
using System.ComponentModel;
using System.Text.Json.Serialization;

namespace Campus_System_WebApi.Services
{
    public class LoginService
    {
        private readonly IGoCollection<UserEntity> _userCollection;
        private readonly TokenProcessor _tokenProcessor;

        public LoginService(IGoCollection<UserEntity> userCollection,
                            TokenProcessor tokenProcessor)
        {
            this._userCollection = userCollection;
            this._tokenProcessor = tokenProcessor;
        }

        internal async Task<LoginCreatorResponse> LoginCreator(LoginCreatorRequest request)
        {
            var userEntity = await _userCollection.FindOneAsync(filter: user => user.Email == request.Email.ToLower() &&
                                                                                user.Password == request.Password,
                                                                projection: projecter => projecter.Include(user => user.Id)
                                                                                                  .Include(user => user.Role)
                                                                                                  .Include(user => user.Status)) ?? throw new CustomException("信箱或密碼錯誤", 401);

            if (userEntity.Status == Campus_System_Database_Model.DocStatus.inactive) throw new CustomException("此帳號已被停權", 403);
            if (userEntity.Role != UserRole.creator) throw new CustomException($"登入身分不正確, 請以 {UserRole.creator} 登入");
            
            var token = _tokenProcessor.CreateAuthToken(userEntity);

            return new LoginCreatorResponse
            {
                Token = token,
            };
        }

        internal async Task<LoginAdminResponse> LoginAdmin(LoginAdminRequest request)
        {
            var userEntity = await _userCollection.FindOneAsync(filter: user => user.Email == request.Email.ToLower() &&
                                                                    user.Password == request.Password,
                                                    projection: projecter => projecter.Include(user => user.Id)
                                                                                      .Include(user => user.Role)
                                                                                      .Include(user => user.Status)) ?? throw new CustomException("信箱或密碼錯誤", 401);

            if (userEntity.Status == Campus_System_Database_Model.DocStatus.inactive) throw new CustomException("此帳號已被停權", 403);
            if (userEntity.Role != UserRole.admin) throw new CustomException($"登入身分不正確, 請以 {UserRole.admin} 登入");

            var token = _tokenProcessor.CreateAuthToken(userEntity);

            return new LoginAdminResponse
            {
                Token = token,
            };
        }

        internal async Task<LoginManagerResponse> LoginManager(LoginManagerRequest request)
        {
            var userEntity = await _userCollection.FindOneAsync(filter: user => user.Email == request.Email.ToLower() &&
                                                                                user.Password == request.Password,
                                                                projection: projecter => projecter.Include(user => user.Id)
                                                                                                  .Include(user => user.Role)
                                                                                                  .Include(user => user.Status)) ?? throw new CustomException("信箱或密碼錯誤", 401);

            if (userEntity.Status == Campus_System_Database_Model.DocStatus.inactive) throw new CustomException("此帳號已被停權", 403);
            if (userEntity.Role != UserRole.manager) throw new CustomException($"登入身分不正確, 請以 {UserRole.manager} 登入");

            var token = _tokenProcessor.CreateAuthToken(userEntity);

            return new LoginManagerResponse
            {
                Token = token,
            };
        }

        internal async Task<LoginTeacherResponse> LoginTeacher(LoginTeacherRequest request)
        {
            var userEntity = await _userCollection.FindOneAsync(filter: user => user.Email == request.Email.ToLower() &&
                                                                                user.Password == request.Password,
                                                                projection: projecter => projecter.Include(user => user.Id)
                                                                                                  .Include(user => user.Role)
                                                                                                  .Include(user => user.Status)) ?? throw new CustomException("信箱或密碼錯誤", 401);

            if (userEntity.Status == Campus_System_Database_Model.DocStatus.inactive) throw new CustomException("此帳號已被停權", 403);
            if (userEntity.Role != UserRole.teacher) throw new CustomException($"登入身分不正確, 請以 {UserRole.teacher} 登入");

            var token = _tokenProcessor.CreateAuthToken(userEntity);

            return new LoginTeacherResponse
            {
                Token = token,
            };
        }

        internal async Task<LoginStudentResponse> LoginStudent(LoginStudentRequest request)
        {
            var userEntity = await _userCollection.FindOneAsync(filter: user => user.Email == request.Email.ToLower() &&
                                                                                user.Password == request.Password,
                                                                projection: projecter => projecter.Include(user => user.Id)
                                                                                                  .Include(user => user.Role)
                                                                                                  .Include(user => user.Status)) ?? throw new CustomException("信箱或密碼錯誤", 401);

            if (userEntity.Status == Campus_System_Database_Model.DocStatus.inactive) throw new CustomException("此帳號已被停權", 403);
            if (userEntity.Role != UserRole.student) throw new CustomException($"登入身分不正確, 請以 {UserRole.student} 登入");

            var token = _tokenProcessor.CreateAuthToken(userEntity);

            return new LoginStudentResponse
            {
                Token = token,
            };
        }
    }

    public class LoginCreatorRequest
    {
        [JsonPropertyName("email")]
        [DefaultValue("信箱")]
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
        [DefaultValue("信箱")]
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
        [DefaultValue("信箱")]
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
        [DefaultValue("信箱")]
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
        [DefaultValue("信箱")]
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
