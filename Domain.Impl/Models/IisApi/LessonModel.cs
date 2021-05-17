using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Impl.Models.IisApi
{
    public class LessonModel
    {
        public List<int> WeekNumber { get; set; }
        public int NumSubgroup { get; set; }
        public List<string> Auditory { get; set; }
        public string LessonTime { get; set; }
        public string Subject { get; set; }
        public string LessonType { get; set; }
    }
}
