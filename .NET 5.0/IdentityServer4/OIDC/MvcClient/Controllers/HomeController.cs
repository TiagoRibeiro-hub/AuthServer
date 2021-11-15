using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MvcClient.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http;
using IdentityModel.Client;

namespace MvcClient.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IHttpClientFactory _httpClient;

        public HomeController(ILogger<HomeController> logger, IHttpClientFactory httpClient)
        {
            _logger = logger;
            _httpClient = httpClient;
        }

        public IActionResult Index()
        {
            return View();
        }

        [Authorize]
        public async Task<IActionResult> Privacy()
        {
            var accessToken = await HttpContext.GetTokenAsync("access_token");
            var id_Token = await HttpContext.GetTokenAsync("id_token");

            var _accessToken = new JwtSecurityTokenHandler().ReadJwtToken(accessToken);
            var _id_Token = new JwtSecurityTokenHandler().ReadJwtToken(id_Token);

            var res = await GetSecret(accessToken);
            await RefreshToken();

            return View();
        }

        public async Task RefreshToken()
        {
            var refreshToken = await HttpContext.GetTokenAsync("refresh_token");

            var serverClient = _httpClient.CreateClient();
            var discoverDocument = await serverClient.GetDiscoveryDocumentAsync("https://localhost:44380/");

            var refreshTokenClient = _httpClient.CreateClient();

            var tokenResponse = await refreshTokenClient.RequestRefreshTokenAsync(
                new RefreshTokenRequest
                {
                    Address = discoverDocument.TokenEndpoint,
                    RefreshToken = refreshToken,
                    ClientId = "client_id_mvc",
                    ClientSecret = "client_secret_mvc"
                });

            var authInfo = await HttpContext.AuthenticateAsync("Cookie");

            authInfo.Properties.UpdateTokenValue("access_token", tokenResponse.AccessToken);
            authInfo.Properties.UpdateTokenValue("refresh_token", tokenResponse.RefreshToken);
            // not important
            authInfo.Properties.UpdateTokenValue("id_token", tokenResponse.IdentityToken);

            await HttpContext.SignInAsync("Cookie", authInfo.Principal, authInfo.Properties);
        }

        public async Task<string> GetSecret(string accessToken)
        {

            var apiClient = _httpClient.CreateClient();

            apiClient.SetBearerToken(accessToken);

            var response = await apiClient.GetAsync("https://localhost:44359/api/secret");

            var content = await response.Content.ReadAsStringAsync();
            
            // e.g just to confirm authorization
            return content;
        }

        public IActionResult LogOut()
        {
            //.RemoveCookie("Cookie")
            //.RemoveOpenIdConnect("oidc")
            return SignOut("Cookie", "oidc");
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
