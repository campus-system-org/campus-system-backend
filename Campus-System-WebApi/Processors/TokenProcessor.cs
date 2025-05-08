using Campus_System_Database_Model.Data;
using MongoDB.Bson;
using System.Text.Json;

namespace Campus_System_WebApi.Processors
{
    public class TokenProcessor
    {
        private readonly StringEncryptor _stringEncryptor;

        public TokenProcessor(StringEncryptor stringEncryptor)
        {
            this._stringEncryptor = stringEncryptor;
        }

        public string CreateAuthToken(UserEntity user)
        {
            UserModel userModel = ToUserModel(user);

            var jsonString = JsonSerializer.Serialize(userModel);
            return _stringEncryptor.Encrypt(jsonString);
        }

        public bool TryValidateToken(string token, out UserModel tokenModel)
        {
            try
            {
                var jsonString = _stringEncryptor.Decrypt(token);
                tokenModel = JsonSerializer.Deserialize<UserModel>(jsonString)!;

                return true;
            }
            catch (Exception ex)
            {
                tokenModel = default;
                return false;
            }
        }

        private static UserModel ToUserModel(UserEntity user)
        {
            return new UserModel
            {
                Id = user.Id,
                Role = user.Role
            };
        }
    }

    public class UserModel
    {
        public string Id { get; set; }

        public UserRole Role { get; set; }
    }
}
