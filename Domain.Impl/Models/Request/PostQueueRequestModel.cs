using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Impl.Models.Request
{
    public class PostQueueRequestModel
    {
        public int LessonId { get; set; }
        public string Date { get; set; }
        public string SubGroup { get; set; }
        public string StudentIds { get; set; }
    }
}
