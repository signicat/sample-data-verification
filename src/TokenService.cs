using Microsoft.Extensions.Options;

// TokenService class to handle authentication token requests
public class TokenService(IHttpClientFactory httpClientFactory, IOptions<TokenServiceConfig> configuration)
{
    private readonly TokenServiceConfig _configuration = configuration.Value;

    // Get an authentication token from the token service
    public async Task<TokenResponse?> GetToken()
    {
        var httpClient = httpClientFactory.CreateClient("TokenService");

        // Prepare the request content with client credentials
        var requestContent = new List<KeyValuePair<string, string?>>
        {
            new("grant_type", _configuration.GrantType),
            new("scope", _configuration.Scope),
            new("client_id", _configuration.ClientId),
            new("client_secret", _configuration.ClientSecret)
        };

        // Make the token request
        var httpResponseMessage =
            await httpClient.PostAsync("connect/token", new FormUrlEncodedContent(requestContent));

        // Handle the token response
        if (!httpResponseMessage.IsSuccessStatusCode)
            throw new Exception(httpResponseMessage.StatusCode.ToString());

        // Read and deserialize the token response
        var responseContent = await httpResponseMessage.Content.ReadAsStringAsync();
        return System.Text.Json.JsonSerializer.Deserialize<TokenResponse>(responseContent);
    }
}