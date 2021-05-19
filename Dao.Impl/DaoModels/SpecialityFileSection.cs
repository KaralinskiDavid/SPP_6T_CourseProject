using System;
using System.Collections.Generic;
using System.Text;

namespace Dao.Impl.DaoModels
{
    public class SpecialityFileSection
    {
        public SpecialityFileSection()
        {
            SpecialityFiles = new HashSet<SpecialityFile>();
        }

        public int Id { get; set; }
        public int SpecialityId { get; set; }
        public string Name { get; set; }

        public virtual Speciality Speciality { get; set; }
        public virtual ICollection<SpecialityFile> SpecialityFiles { get; set; }
    }
}
