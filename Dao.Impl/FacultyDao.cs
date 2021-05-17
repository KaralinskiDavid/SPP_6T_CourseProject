using Dao.Impl.DaoModels;
using Dao.Impl.Base;
using System.Threading.Tasks;
using Dao.Impl.DaoModels.Context;
using Microsoft.EntityFrameworkCore;

namespace Dao.Impl
{
    public class FacultyDao : BaseDao<Faculty>, IFacultyDao<Faculty>
    {
        public FacultyDao(DaoContext context) : base(context) { }

        public override async Task<Faculty> GetItemByIdAsync(int id) => await _context.Faculties.FirstOrDefaultAsync(i => i.Id == id);
    }
}
