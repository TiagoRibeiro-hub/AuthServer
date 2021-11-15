using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace TokenService
{
    public class AccessTokenGenerator
    {
        private readonly TokenGenerator _tokenGenerator;
        private readonly IConfiguration _configuration;

        public AccessTokenGenerator(TokenGenerator tokenGenerator,
                IConfiguration configuration)
        {
            _tokenGenerator = tokenGenerator;
            _configuration = configuration;
        }

        public (string, JwtSecurityToken) GenerateAccessToken(IdentityUser user, IList<string> userRoles)
        {
            var claims = new List<Claim>();

            claims.Add(new Claim(ClaimTypes.NameIdentifier, user.Id));
            claims.Add(new Claim(ClaimTypes.Name, user.UserName));
            claims.Add(new Claim(ClaimTypes.Email, user.Email));

            foreach (var item in userRoles)
            {
                claims.Add(new Claim(ClaimTypes.Role, item.ToString()));
            };

            //int AccessTokenExpiryMin = int.Parse(_configuration["AuthTokenSettings:AccessTokenExpiryMin"]);
            int AccessTokenExpiryMin = 1;
            return _tokenGenerator.GenerateToken(_configuration["AuthTokenSettings:Key"], _configuration["AuthTokenSettings:Issuer"],
                                                    _configuration["AuthTokenSettings:Audience"], AccessTokenExpiryMin, claims);
        }

    }
}
