using Microsoft.AspNetCore.Identity;
using SportDataAuth.DbContext.Models;
using SportDataAuth.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TokenService
{
    public class UserValidator
    {
        private readonly AccessTokenGenerator _accessTokenGenerator;
        private readonly RefreshTokenGenerator _refreshTokenGenerator;
        private readonly IRefreshTokenService _refreshTokenService;

        public UserValidator(AccessTokenGenerator accessTokenGenerator, RefreshTokenGenerator refreshTokenGenerator,
            IRefreshTokenService refreshTokenService)
        {
            _accessTokenGenerator = accessTokenGenerator;
            _refreshTokenGenerator = refreshTokenGenerator;
            _refreshTokenService = refreshTokenService;
        }

        public async Task<UserManagerResponse> Authenticate(IdentityUser user, IList<string> userRoles)
        {
            (string accessTokenAsString, var accessToken) = _accessTokenGenerator.GenerateAccessToken(user, userRoles);
            var refreshTokenAsString = _refreshTokenGenerator.GenerateRefreshToken();

            RefreshToken token = new RefreshToken
            {
                Token = refreshTokenAsString,
                UserId = user.Id,
            };

            await _refreshTokenService.CreateAsync(token);

            return new UserManagerResponse
            {
                Message = "Login succesfully!",
                Token = accessTokenAsString,
                IsSucess = true,
                ExpiryDate = accessToken.ValidTo,
                RefreshToken = refreshTokenAsString,
            };
        }
    }
}
