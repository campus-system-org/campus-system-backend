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

        private static UserModel ToUserModel(UserEntity user)
        {
            return new UserModel
            {
                Id = user._id,
                Role = user.Role
            };
        }
    }

    public class UserModel
    {
        public ObjectId Id { get; set; }

        public UserRole Role { get; set; }
    }
}
