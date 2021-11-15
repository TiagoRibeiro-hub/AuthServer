using Microsoft.EntityFrameworkCore;
using SportDataAuth.DbContext;
using SportDataAuth.DbContext.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TokenService
{
    public class RefreshTokenService: IRefreshTokenService
    {
        private readonly AuthDbContext _db;

        public RefreshTokenService(AuthDbContext db)
        {
            _db = db;
        }

        public async Task CreateAsync(RefreshToken refreshToken)
        {
            _db.RefreshTokens.Add(refreshToken);
            await _db.SaveChangesAsync();
        }

        public async Task<RefreshToken> GetByTokenAsync(string token)
        {
            RefreshToken refreshToken = await _db.RefreshTokens.FirstOrDefaultAsync(x => x.Token == token);
            return refreshToken;
        }
        public async Task DeleteAsync(long tokenId)
        {
            RefreshToken refreshToke = await _db.RefreshTokens.FindAsync(tokenId);
            if (refreshToke != null)
            {
                _db.RefreshTokens.Remove(refreshToke);
                await _db.SaveChangesAsync();
            }
        }

        public async Task DeleteAllAsync(string userId)
        {
            IEnumerable<RefreshToken> refreshTokens = await _db.RefreshTokens.Where(x => x.UserId == userId).ToListAsync();
            _db.RefreshTokens.RemoveRange(refreshTokens);
            await _db.SaveChangesAsync();
        }
    }
}
