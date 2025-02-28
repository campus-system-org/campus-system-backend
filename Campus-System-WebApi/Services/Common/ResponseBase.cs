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
}
