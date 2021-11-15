using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SportDataAuth.Responses;
using SportDataAuth.Services;
using SportDataAuth.Services.AdminServices;
using SportDataAuth.Services.ApiExceptionsServices;
using SportDataAuth.Shared.ApiResponses;
using SportDataAuth.Shared.AuthRequests;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace SportDataAuth.Controllers
{
    [Route("api/signIn")]
    [ApiController]
    public class SignInController : ControllerBase
    {
        public readonly IMyAuthenticationService _authenticationService;
        private readonly IApiExceptionsServices _apiExceptionsServices;

        private const string controller = "SignInController";

        public SignInController(IMyAuthenticationService authenticationService, IApiExceptionsServices apiExceptionsServices)
        {
            _authenticationService = authenticationService;
            _apiExceptionsServices = apiExceptionsServices;
        }


        // Register
        // api/signIn/register
        [AllowAnonymous]
        [HttpPost("register")]
        [ProducesResponseType(200, Type = typeof(ApiResponse))]
        [ProducesResponseType(400, Type = typeof(ApiResponseError))]
        public async Task<ActionResult<ApiResponse>> RegisterAsync([FromBody] RegisterRequest register)
        {
            try
            {
                var result = await _authenticationService.RegisterUserAsync(register);

                if (result.IsSucess)
                    return result.AsApiResponseDto();

                return result.AsApiResponseErrorDto();

            }
            catch (Exception ex)
            {
                return await _apiExceptionsServices.SendExceptionByEmailAsync(ex, controller, "RegisterAsync");
            }

        }

        // CONFIRM EMAIL
        // api/signIn/confirEmail
        [HttpGet("confirmEmail")]
        [ProducesResponseType(200, Type = typeof(ApiResponse))]
        [ProducesResponseType(400, Type = typeof(ApiResponseError))]
        public async Task<ActionResult<ApiResponse>> ConfirmEmail(string userId, string token, string urlClientPage)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(userId) || string.IsNullOrWhiteSpace(token))
                    return new UserManagerResponse
                    {
                        Message = "Somenthing wrong with url confirmEmail",
                        IsSucess = false,
                    }.AsApiResponseErrorDto();

                var result = await _authenticationService.ConfirmEmailAsync(userId, token);

                if (result.IsSucess)
                {
                    return result.AsApiResponseDto();
                    //Redirect confirm email page client side
                }

                return result.AsApiResponseErrorDto();
            }
            catch (Exception ex)
            {
               return await _apiExceptionsServices.SendExceptionByEmailAsync(ex, controller, "ConfirmEmail");
            }

        }
    }
}
