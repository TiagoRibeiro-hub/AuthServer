using Microsoft.AspNetCore.Mvc;
using SportDataAuth.DbContext.Models;
using SportDataAuth.Responses;
using SportDataAuth.Shared.ApiResponses;
using SportDataAuth.Shared.AuthRequests;
using System.Threading.Tasks;

namespace SportDataAuth.Services
{
    public interface IMyAuthenticationService
    {
        // SignIn
        Task<UserManagerResponse> RegisterUserAsync(RegisterRequest register);
        Task<UserManagerResponse> ConfirmEmailAsync(string userId, string token);
        Task<UserManagerResponse> ConfirmPhoneAsync();

        // LogIn
        Task<UserManagerResponse> LoginUserAsync(LoginRequest login);
        Task<UserManagerResponse> LogOutAsync(string userId);
        Task<UserManagerResponse> RefreshTokenAsync(string refreshTokenShared);
    }
}
