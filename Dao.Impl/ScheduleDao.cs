using Dao.Impl.DaoModels;
using Dao.Impl.Base;
using System.Threading.Tasks;
using Dao.Impl.DaoModels.Context;
using System;
using Microsoft.EntityFrameworkCore;

namespace Dao.Impl
{
    public class ScheduleDao : BaseDao<Schedule>, IScheduleDao<Schedule>
    {
        public ScheduleDao(DaoContext context) : base(context) { }

        public override Task<Schedule> GetItemByIdAsync(int id) => throw new NotImplementedException();

        public async Task<Schedule> GetScheduleByGroupId(int groupId) => await _context.Schedules
            .Include(s=>s.DaySchedules).ThenInclude(ds=>ds.Lessons).ThenInclude(l=>l.LessonType)
            .Include(s => s.DaySchedules).ThenInclude(ds => ds.Lessons).ThenInclude(l => l.Queues)
            .FirstOrDefaultAsync(s => s.GroupId == groupId);
    }
}
