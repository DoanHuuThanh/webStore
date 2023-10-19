using doanhuuthanh_web.ViewModel.System.Users;
using Newtonsoft.Json;
using System.Text;

namespace doanhuuthanh_web.addminWeb.Services
{
    public class UserApiClient : IUserApiClient
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public UserApiClient(IHttpClientFactory httpClientFactory) { 
        _httpClientFactory = httpClientFactory;
        }
        public async Task<string> Authencate(LoginRequest request)
        {
            var json = JsonConvert.SerializeObject(request);
            var httpContent = new StringContent(json,Encoding.UTF8,"application/json");
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri("http://localhost:5199");
            var respone =await client.PostAsync("/api/Users/authenticate",httpContent);
            var token = await respone.Content.ReadAsStringAsync();
            return token;
        }
    }
}
