using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Campus_System_WebApi.Services.Common
{
    public class PagedRequestBase
    {
        [JsonPropertyName("paged_info")]
        public PagedRequestPagedInfo PagedInfo { get; set; }
    }

    public class PagedRequestPagedInfo
    {
        [JsonPropertyName("page")]
        [DefaultValue(1)]
        [Range(1, int.MaxValue, ErrorMessage = "page 必須大於等於 1")]
        public int Page { get; set; }
    }
}
