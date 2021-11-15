using EmailService;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Configuration;
using SportDataAuth.Responses;
using SportDataAuth.Shared.ApiResponses;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace SportDataAuth.Services.ApiExceptionsServices
{
    public class ApiExceptionsServices : IApiExceptionsServices
    {
        private readonly IConfiguration _configuration;
        private readonly IEmailSender _emailService;

        public ApiExceptionsServices(
            IConfiguration configuration, IEmailSender emailService)
        {
            _configuration = configuration;
            _emailService = emailService;
        }
        public async Task<ApiResponseError> SendExceptionByEmailAsync(Exception ex, string controller, string function)
        {
            var message = new Message(new string[] { _configuration["EmailSettings:To"] }, $"Error {controller}", $"<h3>Function {function}</h3></br><p>{ex}</p>");

            await _emailService.SendEmailAsync(message);

            return new UserManagerResponse
            {
                Message = "Something went wrong. See email",
                IsSucess = false,
            }.AsApiResponseErrorDto();
        }

    }
}
