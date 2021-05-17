using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dao.Impl.DaoModels
{
    public class Faculty
    {
        public Faculty() { Specialities = new HashSet<Speciality>(); }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Abbreviation { get; set; }

        public virtual ICollection<Speciality> Specialities { get; set; }
    }
}
