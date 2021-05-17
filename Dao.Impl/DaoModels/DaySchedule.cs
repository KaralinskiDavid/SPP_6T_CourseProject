using System.Collections.Generic;

namespace Dao.Impl.DaoModels
{
    public class DaySchedule
    {
        public DaySchedule()
        {
            Lessons = new HashSet<Lesson>();
        }

        public int Id { get; set; }
        public int DayNumber { get; set; }
        public int ScheduleId { get; set; }

        public virtual Schedule Schedule { get; set; }
        public virtual ICollection<Lesson> Lessons { get; set; }
    }
}
