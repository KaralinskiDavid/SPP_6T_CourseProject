using Dao.Base;
using System.Threading.Tasks;

namespace Dao
{
    public interface IScheduleDao<T> : IBaseDao<T> where T : class
    {
        public Task<T> GetScheduleByGroupId(int groupId);
    }
}
