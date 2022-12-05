using Microsoft.Net.Http.Headers;
using System.Text.Json;

namespace HealthApplication.Services
{
    public class SupaAuthService : ISupaAuthService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly string authURL;
        private readonly string apiKey;

        public SupaAuthService(IHttpClientFactory httpClientFactory) 
        {
            _httpClientFactory= httpClientFactory;
            authURL = Environment.GetEnvironmentVariable("SUPABASE_AUTH_URL");
            apiKey = Environment.GetEnvironmentVariable("SUPABASE_API_KEY");
        }

        /// <summary>
        /// Verify if user is verified with JWT token
        /// </summary>
        /// <param name="jwt"></param>
        /// <returns></returns>
        public async Task<bool> VerifyUser(string jwt)
        {
            string route = "/user";

            var httpRequestMessage = new HttpRequestMessage(
                HttpMethod.Get,
                authURL + route)
            {
                Headers =
                {
                    { HeaderNames.Authorization, "Bearer " + jwt },
                    { "apiKey", apiKey }
                }
            };

            var httpClient = _httpClientFactory.CreateClient();

            var httpResponseMessage = await httpClient.SendAsync(httpRequestMessage);

            // If api call returns OK, user is authenticated
            return httpResponseMessage.IsSuccessStatusCode;

        }
    }
}
