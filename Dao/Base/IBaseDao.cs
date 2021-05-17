using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dao.Base
{
    public interface IBaseDao<T>
    {
        Task<T> GetItemByIdAsync(int id);
        Task<IQueryable<T>> GetItemsAsync();
        Task<int> CreateAsync(T item);
        Task<int> CreateRangeAsync(IEnumerable<T> items);
        Task<int> UpdateAsync(T item);
        Task<int> UpdateRangeAsync(IEnumerable<T> items);
        Task<int> RemoveAsync(T item);
        Task<int> RemoveRangeAsync(IEnumerable<T> items);
    }
}
