using System.Net.Http;
using System.Threading.Tasks;

namespace Common.Implementations.InnerHttpClient
{
    public class IdentityHttpService : HttpService
    {
        public IdentityHttpService(HttpClient httpClient, TokenProvider token)
            : base(httpClient, token)
        {
            _httpClient = httpClient;
        }

        private readonly HttpClient _httpClient;

        public async Task<bool> IsUserExistsAsync(string userName)
        {
            await SetTokenAsync();



            HttpResponseMessage response = await _httpClient.GetAsync("api/users/" + userName + "/exist");
            return response.IsSuccessStatusCode;
        }
    }
}
