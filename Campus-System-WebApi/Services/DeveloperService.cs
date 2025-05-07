
using Campus_System_Database_Model.Data;
using System.ComponentModel;
using System.Text.Json.Serialization;

namespace Campus_System_WebApi.Services
{
    public class DeveloperService
    {
        internal async Task CreateUser(DeveloperCreateUserRequest request)
        {
            throw new NotImplementedException();
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
