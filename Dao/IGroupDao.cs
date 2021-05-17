using Dao.Base;
using System.Threading.Tasks;

namespace Dao
{
    public interface IGroupDao<T> : IBaseDao<T> where T: class
    {
        public Task<T> GetGroupByNumber(string number);
    }
}
