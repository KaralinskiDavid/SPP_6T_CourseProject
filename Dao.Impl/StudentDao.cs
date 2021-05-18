using Dao.Impl.Base;
using Dao.Impl.DaoModels;
using Dao.Impl.DaoModels.Context;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Dao.Impl
{
    public class StudentDao : BaseDao<Student>, IStudentDao<Student>
    {
        public StudentDao(DaoContext context) : base(context) { }

        public override async Task<Student> GetItemByIdAsync(int id) => await _context.Students.FirstOrDefaultAsync(i => i.Id == id);

        public async Task<Student> GetStudentByUserId(string userId) => await _context.Students.Include(s=>s.Group).FirstOrDefaultAsync(i => i.UserId == userId);
    }
}
