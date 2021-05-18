using Dao.Impl.DaoModels;
using Dao.Impl.Base;
using System.Threading.Tasks;
using Dao.Impl.DaoModels.Context;
using Microsoft.EntityFrameworkCore;

namespace Dao.Impl
{
    public class QueueDao : BaseDao<Queue>, IQueueDao<Queue>
    {
        public QueueDao(DaoContext context) : base(context) { }
        public override async Task<Queue> GetItemByIdAsync(int id) => await _context.Queues.FirstOrDefaultAsync(i => i.Id == id);
    }
}
