using System.Text.Json.Serialization;

namespace InvoicierWebApiV1.Data.AuthModels
{
    public class RefreshTokenRequest
    {

        [JsonPropertyName("refreshToken")]
        public string RefreshToken { get; set; }
    }
}
