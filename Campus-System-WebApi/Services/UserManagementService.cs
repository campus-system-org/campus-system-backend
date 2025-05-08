using Campus_System_Database_Model.Data;
using Campus_System_WebApi.Processors;
using Campus_System_WebApi.Services.Common;
using MongoGogo.Connection;
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
}
