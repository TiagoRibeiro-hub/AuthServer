using System;
using System.Collections.Generic;
using System.Security.Claims;

namespace SportDataAuth.Responses
{
    public class UserManagerResponse
    {
        public string Message { get; set; }
        public bool IsSucess { get; set; }
        public IEnumerable<string> Errors { get; set; }
        public string Token { get; set; }
        public DateTime? ExpiryDate { get; set; }
        public string RefreshToken { get; set; }
        public IEnumerable<Claim> Claims { get; set; }


    }
}
