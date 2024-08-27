using RestSharp; // For RestSharp library
using Newtonsoft.Json; // For JSON serialization/deserialization


namespace BookStoreApi.Services
{
    public class AuthService
    {
        private readonly string _authUrl = "https://dev-apu414kr0tjtv2uv.us.auth0.com/oauth/token";
        private readonly string _clientId = "MsPjGpUwazasqdSiXhEVHwH57N6bniOO";
        private readonly string _clientSecret = "-sJXc5FMFNn3q0L6VH6FGPZ-6pwQZk2NQNnlF8ihadjfYHHQcf0O-sfLa4EXpBXV";
        private readonly string _audience = "https://localhost:7248/api/Books";

        public async Task<string> GetAccessTokenAsync()
        {
            var client = new RestClient(_authUrl);
            var request = new RestRequest();
            request.AddHeader("content-type", "application/json");

            var body = new
            {
                client_id = _clientId,
                client_secret = _clientSecret,
                audience = _audience,
                grant_type = "client_credentials"
            };

            request.AddJsonBody(body);

            var response = await client.ExecuteAsync(request);

            if (response.IsSuccessful)
            {
                // Parse the response to get the access token
                var tokenResponse = JsonConvert.DeserializeObject<TokenResponse>(response.Content);
                return tokenResponse.AccessToken;
            }
            else
            {
                // Handle errors
                throw new Exception($"Error fetching token: {response.Content}");
            }
        }
    }

    public class TokenResponse
    {
        [JsonProperty("access_token")]
        public string AccessToken { get; set; }
    }

}
