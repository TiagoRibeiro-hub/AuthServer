using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TokenService
{
    public class RefreshTokenGenerator
    {
        private readonly TokenGenerator _tokenGenerator;
        private readonly IConfiguration _configuration;

        public RefreshTokenGenerator(TokenGenerator tokenGenerator, IConfiguration configuration)
        {
            _tokenGenerator = tokenGenerator;
            _configuration = configuration;
        }
        public string GenerateRefreshToken()
        {
            //int RefreshAccessTokenExpiryMin = int.Parse(_configuration["AuthTokenSettings:RefreshAccessTokenExpiryMin"]);
            int RefreshAccessTokenExpiryMin = 15;
            (var refreshTokenAsString, var refreshToken) = _tokenGenerator.GenerateToken(_configuration["AuthTokenSettings:RefreshKey"], _configuration["AuthTokenSettings:Issuer"],
                                                    _configuration["AuthTokenSettings:Audience"], RefreshAccessTokenExpiryMin, null);

            return refreshTokenAsString;
        }
    }
}
