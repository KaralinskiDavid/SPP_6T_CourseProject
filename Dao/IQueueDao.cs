using Dao.Base;

namespace Dao
{
    public interface IQueueDao<T> : IBaseDao<T> where T: class
    {
    }
}
