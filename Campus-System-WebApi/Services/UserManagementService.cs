using Campus_System_WebApi.Processors;
using Campus_System_WebApi.Services.Common;
using System.Text.Json.Serialization;

namespace Campus_System_WebApi.Services
{
    public class UserManagementService
    {
        internal async Task<UserManagementGetListResponse> GetList(UserModel currentUser)
        {
            throw new NotImplementedException();
        }
    }

    public class UserManagementGetListResponse : PagedResponseBase
    {
        [JsonPropertyName("users")]
        public List<UserManagementGetListResponseUser> Users { get; set; }
    }

    public class UserManagementGetListResponseUser
    {
    }
}
