namespace Dao.Impl.DaoModels
{
    public class Queue
    {
        public int Id { get; set; }
        public int LessonId { get; set; }
        public string Date { get; set; }
        public string SubGroup { get; set; }
        public string StudentIds { get; set; }

        public virtual Lesson Lesson { get; set; }
    }
}
