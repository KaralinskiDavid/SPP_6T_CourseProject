using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dao.Impl.DaoModels
{
    public class Speciality
    {
        public Speciality() { Groups = new HashSet<Group>(); }


        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }
        [ForeignKey("Faculty")]
        public int FacultyId { get; set; }
        public string Name { get; set; }
        public string Abbreviature { get; set; }
        public string Code { get; set; }
        [ForeignKey("Student")]
        public int? HeadStudentId { get; set; }

        public virtual Faculty Faculty { get; set; }
        public virtual ICollection<Group> Groups { get; set; }
        public virtual Student HeadStudent { get; set; }
    }
}
