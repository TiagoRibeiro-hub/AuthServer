using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace SportDataAuth.Shared.ApiResponses
{
    public class ApiResponseToken : ApiResponse
    {
        public string Token { get; set; }
        public string RefreshToken { get; set; }
        public DateTime ExpiryDate { get; set; }
        public IEnumerable<Claim> Claims { get; set; }

        public ApiResponseToken()
        {
            IsSuccess = true;
        }

        public ApiResponseToken(string token, DateTime date)
        {
            IsSuccess = true;
            Token = token;
            ExpiryDate = date;
        }

        public ApiResponseToken(string message, string token, string refreshToken, DateTime date, IEnumerable<Claim> claims)
        {
            Message = message;
            IsSuccess = true;
            Token = token;
            ExpiryDate = date;
            RefreshToken = refreshToken;
            Claims = claims;
        }
    }
}
