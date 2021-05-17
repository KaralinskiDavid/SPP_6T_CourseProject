using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Impl.Models.Response
{
    public class GetScheduleResponseModel
    {
        public int Id { get; set; }
        public List<DayScheduleModel> DaySchedules { get; set; }
    }
}
