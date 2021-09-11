using System.Text.Json.Serialization;

namespace InvoicierWebApiV1.Data.AuthModels
{
    public class ImpersonationRequest
    {
        [JsonPropertyName("username")]
        public string UserName { get; set; }
    }
}
