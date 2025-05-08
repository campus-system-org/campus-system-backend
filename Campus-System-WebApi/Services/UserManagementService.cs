using Amazon.Runtime.Internal;
using Campus_System_Database_Model;
using Campus_System_Database_Model.Data;
using Campus_System_WebApi.Processors;
using Campus_System_WebApi.Services.Common;
using MongoGogo.Connection;
using System.ComponentModel;
using System.Text.Json.Serialization;
using ZstdSharp.Unsafe;

namespace Campus_System_WebApi.Services
{
    public class UserManagementService
    {
        private readonly IGoCollection<UserEntity> _userCollection;

        public UserManagementService(IGoCollection<UserEntity> userCollection)
        {
            this._userCollection = userCollection;
        }

        internal async Task<UserManagementGetListResponse> GetList(UserManagementGetListRequest request)
        {
            const int pageSize = 30;

            var totalCount = (int)await _userCollection.CountAsync(_ => true);

            var userEntities = await _userCollection.FindAsync(filter: _ => true,
                                                               goFindOption: new GoFindOption<UserEntity>
                                                               {
                                                                   Skip = (request.PagedInfo.Page - 1) * pageSize,
                                                                   Limit = pageSize
                                                               });

            return new UserManagementGetListResponse
            {
                Page = request.PagedInfo.Page,
                PageSize = pageSize,
                TotalPage = (int)Math.Ceiling(totalCount / (double)pageSize),
                Users = userEntities.Select(user => new UserManagementGetListResponseUser
                {
                    Id = user.Id,
                    Email = user.Email,
                    Name = user.Name,
                    CreateTime = user.CreateTime,
                    Telephone = user.Telephone,
                    Role = user.Role.ToString(),
                    Status = user.Status.ToString()
                }).ToList()
            };
        }

        internal async Task CreateOne(UserManagementCreateOneRequest request)
        {
            bool emailExists = await _userCollection.CountAsync(filter: user => user.Email == request.Email) > 0;
            if (emailExists) throw new CustomException("此用戶已存在", 409);

            await _userCollection.InsertOneAsync(new UserEntity
            {
                Id = Guid.NewGuid().ToString(),
                Email = request.Email.ToLower(),
                Name = request.Name,
                Password = request.Password,
                Role = request._UserRole,
                Status = Campus_System_Database_Model.DocStatus.active,
                Telephone = request.Telephone,
                CreateTime = DateTime.UtcNow,
                UpdateTime = null
            });
        }

        internal async Task EditOne(string userId, UserManagementEditOneRequest request)
        {
            bool emailExists = await _userCollection.CountAsync(filter: user => user.Id == userId) > 0;
            if (!emailExists) throw new CustomException("此用戶不存在", 400);

            var bulker = _userCollection.NewBulker();

            if(request.IsActive != null)
            {
                var status = request.IsActive.Value ? DocStatus.active : DocStatus.inactive;

                bulker.UpdateOne(filter: user => user.Id == userId,
                                 updateDefinitionBuilder: updater => updater.Set(user => user.Status, status));
            }
            if(request._UserRole != null)
            {
                bulker.UpdateOne(filter: user => user.Id == userId,
                                 updateDefinitionBuilder: updater => updater.Set(user => user.Role, request._UserRole));
            }

            await bulker.SaveChangesAsync();
        }

        internal async Task DeleteOne(string userId)
        {
            bool emailExists = await _userCollection.CountAsync(filter: user => user.Id == userId) > 0;
            if (!emailExists) throw new CustomException("此用戶不存在", 400);

            await _userCollection.UpdateOneAsync(filter: user => user.Id == userId,
                                                 updateDefinitionBuilder: updater => updater.Set(user => user.Status, Campus_System_Database_Model.DocStatus.inactive));
        }
    }

    public class UserManagementGetListRequest : PagedRequestBase
    {

    }

    public class UserManagementGetListResponse : PagedResponseBase
    {
        [JsonPropertyName("users")]
        public List<UserManagementGetListResponseUser> Users { get; set; }
    }

    public class UserManagementGetListResponseUser
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("email")]
        public string Email { get; set; }

        [JsonPropertyName("telephone")]
        public string? Telephone { get; set; }

        [JsonPropertyName("role")]
        public string Role { get; set; }

        [JsonPropertyName("status")]
        public string Status { get; set; }

        [JsonPropertyName("create_time")]
        public DateTime CreateTime { get; set; }
    }

    public class UserManagementCreateOneRequest
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
        [DefaultValue(@"身分。 可選值: ['creator','admin','manager','teacher','student']")]
        public string Role { get; set; }

        internal UserRole _UserRole => Role.ToEnum<UserRole>();
    }

    public class UserManagementEditOneRequest
    {
        [JsonPropertyName("role")]
        [DefaultValue(@"身分。 可選值: ['creator','admin','manager','teacher','student']")]
        public string? Role { get; set; }

        [JsonPropertyName("is_active")]
        public bool? IsActive { get; set; }

        internal UserRole? _UserRole => Role?.ToEnum<UserRole>();
    }
}
