using Dao.Impl.Base;
using Dao.Impl.DaoModels;
using Dao.Impl.DaoModels.Context;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Dao.Impl
{
    public class SpecialityDao : BaseDao<Speciality>, ISpecialityDao<Speciality>
    {
        public SpecialityDao(DaoContext context) : base(context) { }

        public override async Task<Speciality> GetItemByIdAsync(int id) => await _context.Specialities.FirstOrDefaultAsync(i => i.Id == id);
    }
}
