using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Impl.Models
{
    public class DayScheduleModel
    {
        public int Id { get; set; }
        public int DayNumber { get; set; }
        public int ScheduleId { get; set; }

        public virtual List<LessonModel> Lessons { get; set; }
    }
}
