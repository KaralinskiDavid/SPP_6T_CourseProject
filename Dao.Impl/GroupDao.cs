using Dao.Impl.Base;
using Dao.Impl.DaoModels;
using Dao.Impl.DaoModels.Context;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Dao.Impl
{
    public class GroupDao : BaseDao<Group>, IGroupDao<Group>
    {
        public GroupDao(DaoContext context) : base(context) { }

        public async Task<Group> GetGroupByNumber(string number) => await _context.Groups.FirstOrDefaultAsync(g => g.Number == number);

        public override async Task<Group> GetItemByIdAsync(int id) => await _context.Groups.FirstOrDefaultAsync(i => i.Id == id);
    }
}
