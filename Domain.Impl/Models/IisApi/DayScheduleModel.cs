using System.Collections.Generic;

namespace Domain.Impl.Models.IisApi
{
    public class DayScheduleModel
    {
        public string WeekDay { get; set; }
        public List<LessonModel> Schedule { get; set; }
    }
}
