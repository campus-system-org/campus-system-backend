using System.Text.Json.Serialization;

namespace Campus_System_WebApi.Services.Common
{
    public class ResponseBase<TData>
    {
        [JsonPropertyName("data")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public TData Data { get; set; }

        public ResponseBase(TData data)
        {
            Data = data;
        }
    }

    public class ResponseBase
    {
        [JsonPropertyName("msg")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string Msg { get; set; }
    }

    public class PagedResponseBase<TData> where TData : PagedResponseBase
    {
        [JsonPropertyName("data")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public TData Data { get; set; }

        [JsonPropertyName("total_page")]
        public int TotalPage { get; set; }

        [JsonPropertyName("page")]
        public int Page { get; set; }

        [JsonPropertyName("page_size")]
        public int PageSize { get; set; }

        public PagedResponseBase(TData data)
        {
            Data = data;
            TotalPage = data.TotalPage;
            Page = data.Page;
            PageSize = data.PageSize;
        }
    }

    public class PagedResponseBase
    {
        internal int TotalPage { get; set; }

        internal int Page { get; set; }

        internal int PageSize { get; set; }
    }
}
