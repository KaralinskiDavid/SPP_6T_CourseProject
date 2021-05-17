using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Impl.Models
{
    public class LessonModel
    {
        public int Id { get; set; }
        public int DayScheduleId { get; set; }
        public string LessonTime { get; set; }
        public string SubjectName { get; set; }
        public int SubGroup { get; set; }
        public int LessonTypeId { get; set; }
        public string Auditory { get; set; }
        public string WeekNumber { get; set; }

        public  LessonTypeModel LessonType { get; set; }
    }
}
