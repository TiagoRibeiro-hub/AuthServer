using Microsoft.AspNetCore.Mvc.ModelBinding;
using SportDataAuth.Shared.ApiResponses;
using System;
using System.Threading.Tasks;

namespace SportDataAuth.Services.ApiExceptionsServices
{
    public interface IApiExceptionsServices
    {
        Task<ApiResponseError> SendExceptionByEmailAsync(Exception ex, string controller, string function);

    }
}
