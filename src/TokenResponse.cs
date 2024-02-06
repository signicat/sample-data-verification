using System.Text.Json.Serialization;

// TokenResponse class representing the token response
public class TokenResponse
{
    [JsonPropertyName("access_token")]
    public string? AccessToken { get; set; }
}