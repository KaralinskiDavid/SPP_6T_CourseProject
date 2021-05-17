using Dao.Impl.DaoModels;
using Dao.Impl.Base;
using System.Threading.Tasks;
using Dao.Impl.DaoModels.Context;
using System;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Dao.Impl
{
    public class RefreshTokenDao : BaseDao<RefreshToken>, IRefreshTokenDao<RefreshToken>
    {
        public RefreshTokenDao(DaoContext context) : base(context) { }

        public override Task<RefreshToken> GetItemByIdAsync(int id) =>
            throw new NotImplementedException();

        public Task<RefreshToken> GetItemByToken(string token, string ipAddress)
        {
            var now = DateTime.UtcNow;

            return _context.RefreshTokens.FirstOrDefaultAsync(f => f.ExpirationDate > now && f.RefreshTokenValue == token && f.IpAddress == ipAddress);
        }

        public Task<RefreshToken> GetItemByKey(string key, string ipAddress)
        {
            var now = DateTime.UtcNow;

            return _context.RefreshTokens.FirstOrDefaultAsync(f => f.ExpirationDate > now && f.Key == key && f.IpAddress == ipAddress);
        }

        public async Task<int> RemoveOldTokens(string email)
        {
            _context.RefreshTokens.RemoveRange(_context.RefreshTokens.Where(w => w.Key == email));
            return await _context.SaveChangesAsync();
        }

    }
}
