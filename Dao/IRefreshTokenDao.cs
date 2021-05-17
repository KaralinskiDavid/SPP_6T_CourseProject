using Dao.Base;
using System.Threading.Tasks;

namespace Dao
{
    public interface IRefreshTokenDao<T> : IBaseDao<T> where T:class 
    {
        Task<T> GetItemByToken(string token, string ipAddress);
        Task<T> GetItemByKey(string key, string ipAddress);
        Task<int> RemoveOldTokens(string email);
    }
}
