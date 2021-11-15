using IdentityModel.Client;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Threading.Tasks;

namespace ApiTwoClient.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        private readonly IHttpClientFactory _httpClient;

        public HomeController(IHttpClientFactory httpClient)
        {
            _httpClient = httpClient;
        }

        [Route("/")]
        public async Task<IActionResult> Index()
        {
            // retrieve access token
            var serverClient = _httpClient.CreateClient();

            var discoverDocument = await serverClient.GetDiscoveryDocumentAsync("https://localhost:44380/");

            var tokenResponse = await serverClient.RequestClientCredentialsTokenAsync(
                new ClientCredentialsTokenRequest
                {
                    Address = discoverDocument.TokenEndpoint,

                    ClientId = "client_id",
                    ClientSecret = "client_secret",

                    Scope = "ApiOne",

                });
            
            // retrieve secret data
            var apiClient = _httpClient.CreateClient();

            apiClient.SetBearerToken(tokenResponse.AccessToken);

            var response = await apiClient.GetAsync("https://localhost:44359/secret");

            var content = await response.Content.ReadAsStringAsync();

            return Ok(new
            {
                access_token = tokenResponse.AccessToken,
                message = content
            });
        }
    }
}
