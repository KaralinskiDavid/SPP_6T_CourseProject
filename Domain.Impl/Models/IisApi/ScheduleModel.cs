using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Impl.Models.IisApi
{
    public class ScheduleModel
    {
        public GroupModel StudentGroup { get; set; }
        public List<DayScheduleModel> Schedules { get; set; }
    }
}
