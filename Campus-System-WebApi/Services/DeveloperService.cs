
using Campus_System_Database_Model.Data;
using MongoGogo.Connection;
using System.ComponentModel;
using System.Text.Json.Serialization;

namespace Campus_System_WebApi.Services
{
    public class DeveloperService
    {
        private readonly IGoCollection<UserEntity> _userCollection;

        public DeveloperService(IGoCollection<UserEntity> userCollection)
        {
            this._userCollection = userCollection;
        }

        internal async Task CreateUser(DeveloperCreateUserRequest request)
        {
            //檢查存在性
            var emails = request.Users.Select(user => user.Email.ToLower()).ToArray();

            var currentUserEntities = await _userCollection.FindAsync(filter: user => emails.Contains(user.Email),
                                                                      projection: projecter => projecter.Include(user => user.Email));
            if (currentUserEntities.Any()) 
            {
                throw new CustomException($"以下信箱的帳號已存在: ['{string.Join(",", currentUserEntities.Select(user => user.Email))}']");
            }

            //批次建立帳號
            var now = DateTime.UtcNow;
            var newUsers = request.Users.Select(user => new UserEntity
            {
                Email = user.Email.ToLower(),
                Name = user.Name,
                Password = user.Password,
                Role = user._UserRole,
                Status = Campus_System_Database_Model.DocStatus.active,
                Telephone = user.Telephone,
                CreateTime = now,
                UpdateTime = null
            });
            
            await _userCollection.InsertManyAsync(newUsers);
        }
    }

    public class DeveloperCreateUserRequest
    {
        [JsonPropertyName("users")]
        public List<DeveloperCreateUserRequestUser> Users { get; set; }
    }

    public class DeveloperCreateUserRequestUser
    {
        [JsonPropertyName("email")]
        [DefaultValue("信箱")]
        public string Email { get; set; }

        [JsonPropertyName("name")]
        [DefaultValue("名字")]
        public string Name { get; set; }

        [JsonPropertyName("password")]
        [DefaultValue("密碼")]
        public string Password { get; set; }

        [JsonPropertyName("telephone")]
        [DefaultValue("電話。(可為空值)")]
        public string? Telephone { get; set; }

        [JsonPropertyName("role")]
        [DefaultValue(@"身分。 可選值: ['creator','admin','manager','teacher','student',]")]
        public string Role { get; set; }

        internal UserRole _UserRole => Role.ToEnum<UserRole>();
    }
}
