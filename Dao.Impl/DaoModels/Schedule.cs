using System.Collections.Generic;

namespace Dao.Impl.DaoModels
{
    public class Schedule
    {
        public Schedule()
        {
            DaySchedules = new HashSet<DaySchedule>();
        }

        public int Id { get; set; }
        public int GroupId { get; set; }


        public virtual Group Group { get; set; }
        public virtual ICollection<DaySchedule> DaySchedules { get; set; }
    }
}
