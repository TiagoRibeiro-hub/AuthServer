using SportDataAuth.Shared.ApiResponses;

namespace SportDataAuth.Responses
{
    public static class ExtensionsDto
    {
        public static ApiResponse AsApiResponseDto(this UserManagerResponse x)
        {
            return new ApiResponse
            {
                Message = x.Message,
                IsSuccess = x.IsSucess,
            };
        }
        public static ApiResponseError AsApiResponseErrorDto(this UserManagerResponse x)
        {
            return new ApiResponseError
            {
                Message = x.Message,
                IsSuccess = x.IsSucess,
                Errors = x.Errors
            };
        }
        public static ApiResponseToken AsApiResponseTokenDto(this UserManagerResponse x)
        {
            return new ApiResponseToken
            {
                Message = x.Message,
                IsSuccess = x.IsSucess,
                Token = x.Token,
                ExpiryDate = x.ExpiryDate.Value,
                RefreshToken = x.RefreshToken,
                Claims = x.Claims
            };
        }
    }
}
