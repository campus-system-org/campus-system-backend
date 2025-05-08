using Campus_System_Database_Model.Data;
using Campus_System_WebApi.Processors;
using MongoGogo.Connection;

namespace Campus_System_WebApi.Controllers.Middlewares
{
    public class AuthMiddleware
    {
        public static readonly string USER_Model_KEY = "user-model";

        private readonly RequestDelegate _next;
        private readonly TokenProcessor _tokenProcessor;
        private readonly IGoCollection<UserEntity> _userCollection;

        public AuthMiddleware(RequestDelegate next,
                              TokenProcessor tokenProcessor,
                              IGoCollection<UserEntity> userCollection)
        {
            this._next = next;
            this._tokenProcessor = tokenProcessor;
            this._userCollection = userCollection;
        }

        public async Task Invoke(HttpContext context)
        {
            // 從 Header 中提取 Token
            var token = context.Request.Headers["X-Campus-System-Token"].FirstOrDefault();
            if (string.IsNullOrEmpty(token))
            {
                throw new CustomException("token 錯誤", 401);
            }

            // 驗證 Token 的有效性
            if (!_tokenProcessor.TryValidateToken(token: token, out var tokenModel))
            {
                throw new CustomException("token 錯誤", 401);
            }

            //檢驗有沒有被停權
            var userEntity = await _userCollection.FindOneAsync(filter: user => user.Id == tokenModel.Id,
                                                                projection: projecter => projecter.Include(user => user.Status)
                                                                                                  .Include(user => user.Role)) ?? throw new CustomException("token 錯誤", 401);
            if (userEntity.Status != Campus_System_Database_Model.DocStatus.active) throw new CustomException("此帳號已被停權", 403);

            //存下新的role，因為其值可能隨時會改
            tokenModel.Role = userEntity.Role;

            //存下token model
            context.Items.Add(key: USER_Model_KEY, value: tokenModel);

            await _next(context);
        }
    }
}
