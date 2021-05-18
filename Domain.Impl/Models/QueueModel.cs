using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Impl.Models
{
    public class QueueModel
    {
        public int Id { get; set; }
        public int LessonId { get; set; }
        public string Date { get; set; }
        public string SubGroup { get; set; }
        public string StudentIds { get; set; }
    }
}
