// Create a new WebApplication
var builder = WebApplication.CreateBuilder(args);

// Configure in-memory settings for TokenService
var inMemorySettings = new List<KeyValuePair<string, string?>> {
    new("TokenServiceConfig:GrantType", "client_credentials"),
    new("TokenServiceConfig:Scope", "signicat-api"),
    new("TokenServiceConfig:ClientId",  "<YOUR_CLIENT_ID>"),
    new("TokenServiceConfig:ClientSecret",  "<YOUR_CLIENT_SECRET>")
};

// Build configuration from in-memory settings
var configuration = new ConfigurationBuilder()
    .AddInMemoryCollection(inMemorySettings)
    .Build();

// Configure TokenServiceConfig using the built configuration
builder.Services.Configure<TokenServiceConfig>(
    configuration.GetSection("TokenServiceConfig"));

// Configure HttpClient for TokenService
builder.Services.AddHttpClient("TokenService", httpclient => {
    httpclient.BaseAddress = new Uri("https://api.signicat.com/auth/open/");
});
// Add TokenService as a singleton service
builder.Services.AddSingleton<TokenService>();

// Configure HttpClient for DataVerificationApiService
builder.Services.AddHttpClient("DataVerificationApiService", httpclient => {
    httpclient.BaseAddress = new Uri("https://api.signicat.com/info/lookup/");
});
// Add DataVerificationApiService as a singleton service
builder.Services.AddSingleton<DataVerificationApiService>();

// Build the application
var app = builder.Build();

// Set up a simple root route
app.MapGet("/", () => "Signicat Data Verification Sample App.");

// Set up routes for person and organization information
var person = app.MapGroup("/person");
person.MapGet("/basic/{country}/{source}/{id}", GetPersonBasic);

var organization = app.MapGroup("/organization");
organization.MapGet("/basic/{country}/{source}/{id}", GetOrganizationBasic);

// Run the application
app.Run();

// Handler for getting basic person information
static async Task<IResult> GetPersonBasic(string country, string source, string id, DataVerificationApiService dataVerificationApiService)
{
    var response = await dataVerificationApiService.GetPersonBasic(country, source, id);
    return TypedResults.Ok(response);
}

// Handler for getting basic organization information
static async Task<IResult> GetOrganizationBasic(string country, string source, string id, DataVerificationApiService dataVerificationApiService)
{
    var response = await dataVerificationApiService.GetOrganizationBasic(country, source, id);
    return TypedResults.Ok(response);
}