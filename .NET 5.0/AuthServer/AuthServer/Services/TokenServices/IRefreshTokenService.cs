using SportDataAuth.DbContext.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TokenService
{
    public interface IRefreshTokenService
    {
        Task CreateAsync(RefreshToken refreshToken);
        Task<RefreshToken> GetByTokenAsync(string token);
        Task DeleteAsync(long tokenId);
        Task DeleteAllAsync(string userId);
    }
}
