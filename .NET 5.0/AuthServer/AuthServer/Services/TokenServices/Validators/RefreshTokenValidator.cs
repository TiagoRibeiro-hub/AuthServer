using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TokenService
{
    public class RefreshTokenValidator
    {
        private readonly IConfiguration _configuration;

        public RefreshTokenValidator(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public bool ValidateRefreshToken(string refreshToken)
        {
            TokenValidationParameters validationParameters = new TokenValidationParameters
            {
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["AuthTokenSettings:RefreshKey"])),
                ValidAudience = _configuration["AuthTokenSettings:Audience"],
                ValidIssuer = _configuration["AuthTokenSettings:Issuer"],
                ValidateIssuerSigningKey = true,
                ValidateAudience = true,
                ValidateIssuer = true,
                RequireExpirationTime = true,
                ClockSkew = TimeSpan.Zero,
            };

            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();

            try
            {
                tokenHandler.ValidateToken(refreshToken, validationParameters, out SecurityToken securityToken);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
