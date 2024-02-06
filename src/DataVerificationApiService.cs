// DataVerificationApiService class to handle API requests for person and organization information
public class DataVerificationApiService(IHttpClientFactory httpClientFactory, TokenService tokenService)
{
    // Get basic person information from the API
    public async Task<string> GetPersonBasic(string country, string source, string id)
    {
        return await MakeRequest($"countries/{country}/persons?identityNumber={id}&source={source}");
    }

    // Get basic organization information from the API
    public async Task<string> GetOrganizationBasic(string country, string source, string id)
    {
        return await MakeRequest($"countries/{country}/organizations/{id}?source={source}");
    }

    // Helper method to make an authenticated API request
    private async Task<string> MakeRequest(string url)
    {
        var httpClient = httpClientFactory.CreateClient("DataVerificationApiService");
        
        // Get an authentication token from the TokenService
        TokenResponse? token;
        try
        {
            token = await tokenService.GetToken();

            if (token == null) return "Something went wrong fetching the authentication token";
        }
        catch (Exception ex)
        {
            return ex.Message;
        }
        
        // Set the authorization header with the access token
        httpClient.DefaultRequestHeaders.Authorization =
            new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token.AccessToken);

        // Make the API request
        var httpResponseMessage = await httpClient.GetAsync(url);

        // Handle the API response
        if (!httpResponseMessage.IsSuccessStatusCode)
        {
            if (httpResponseMessage.StatusCode == System.Net.HttpStatusCode.NotFound)
                return "The entity you tried to look up was not found at the source.";
            else 
                return httpResponseMessage.StatusCode + ", " + httpResponseMessage.ReasonPhrase;
        }

        // Read and return the response content
        var responseContent = await httpResponseMessage.Content.ReadAsStringAsync();

        return responseContent;
    }
}