using EmailService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SportDataAuth.Responses;
using SportDataAuth.Services;
using SportDataAuth.Services.ApiExceptionsServices;
using SportDataAuth.Shared.ApiResponses;
using SportDataAuth.Shared.AuthRequests;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace SportDataAuth.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class LogInController : ControllerBase
    {
        public readonly IMyAuthenticationService _authenticationService;
        public readonly IApiExceptionsServices _apiExceptionsServices;
        private readonly IEmailSender _emailService;

        private const string controller = "LogInController";

        public LogInController(IMyAuthenticationService authenticationService, IApiExceptionsServices apiExceptionsServices, IEmailSender emailService)
        {
            _authenticationService = authenticationService;
            _apiExceptionsServices = apiExceptionsServices;
            _emailService = emailService;
        }

        // LogIn
        // api/auth/login
        [AllowAnonymous]
        [HttpPost("Login")]
        [ProducesResponseType(200, Type = typeof(ApiResponseToken))]
        [ProducesResponseType(400, Type = typeof(ApiResponseError))]
        public async Task<ActionResult<ApiResponse>> LoginAsync([FromBody] LoginRequest login)
        {
            try
            {
                var result = await _authenticationService.LoginUserAsync(login);

                var message = new Message(new string[] { login.Email }, "New login", "<h1>Hey! New Login at your account</h1><p>Login at your account at" + DateTime.Now + "</p>");
                await _emailService.SendEmailAsync(message);

                if (result.IsSucess)
                    return result.AsApiResponseTokenDto();

                return result.AsApiResponseErrorDto();

            }
            catch (Exception ex)
            {
                return await _apiExceptionsServices.SendExceptionByEmailAsync(ex, controller, "LoginAsync");
            }

        }

        // LogOut
        // api/auth/logout
        [HttpDelete("LogOut")]
        [ProducesResponseType(200, Type = typeof(ApiResponse))]
        [ProducesResponseType(400, Type = typeof(ApiResponseError))]
        public async Task<ActionResult<ApiResponse>> LogOutAsync(string userId)
        {
            try
            {
                var result = await _authenticationService.LogOutAsync(userId);

                if (result.IsSucess)
                    return result.AsApiResponseDto();

                return result.AsApiResponseErrorDto();


            }
            catch (Exception ex)
            {
                return await _apiExceptionsServices.SendExceptionByEmailAsync(ex, controller, "LogOutAsync");
            }

        }

        // Refresh
        // api/auth/refresh
        [HttpPost("Refresh")]
        [ProducesResponseType(200, Type = typeof(ApiResponseToken))]
        [ProducesResponseType(400, Type = typeof(ApiResponseError))]
        public async Task<ActionResult<ApiResponse>> RefreshTokenAsync([FromBody] RefreshTokenRequest refreshTokenRequest)
        {
            try
            {
                var result = await _authenticationService.RefreshTokenAsync(refreshTokenRequest.RefreshToken);

                if (result.IsSucess)
                    return result.AsApiResponseTokenDto();

                return result.AsApiResponseErrorDto();

            }
            catch (Exception ex)
            {
                return await _apiExceptionsServices.SendExceptionByEmailAsync(ex, controller, "RefreshTokenAsync");
            }

        }
    }
}
