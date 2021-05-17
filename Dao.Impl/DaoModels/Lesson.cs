using System.Collections.Generic;

namespace Dao.Impl.DaoModels
{
    public class Lesson
    {
        public int Id { get; set; }
        public int DayScheduleId { get; set; }
        public string LessonTime { get; set; }
        public string SubjectName { get; set; }
        public int SubGroup { get; set; }
        public int LessonTypeId { get; set; }
        public string Auditory { get; set; }
        public string WeekNumber { get; set; }

        public virtual LessonType LessonType { get; set; }
        public virtual DaySchedule DaySchedule { get; set; }
    }
}
