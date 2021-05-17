using System.ComponentModel.DataAnnotations.Schema;

namespace Dao.Impl.DaoModels
{
    public class Student
    {
        public int Id { get; set; }
        [ForeignKey("Group")]
        public int GroupId { get; set; }
        public int? SubGroup { get; set; }
        public string UserId { get; set; }
        public int? SpecialityId { get; set; }
        public virtual Group Group { get; set; }
        public virtual Speciality Speciality { get; set; }
    }
}
