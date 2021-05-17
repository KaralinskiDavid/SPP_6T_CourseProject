using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dao.Impl.DaoModels
{
    public class Group
    {
        public Group() { Students = new HashSet<Student>(); }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }
        public int Course { get; set; }
        public int SpecialityId { get; set; }
        public string Number { get; set; }
        [ForeignKey("Student")]
        public int? HeadStudentId { get; set; }
        public int? ScheduleId { get; set; }

        public virtual Speciality Speciality { get; set; }
        public virtual Student Student { get; set; }
        public virtual ICollection<Student> Students { get; set; }
        public virtual Schedule Schedule { get; set; }
    }
}
