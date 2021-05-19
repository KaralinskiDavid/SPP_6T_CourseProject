using System;
using System.Collections.Generic;
using System.Text;

namespace Dao.Impl.DaoModels
{
    public class SpecialityFile
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Path { get; set; }
        public int SpecialityFileSectionId { get; set; }

        public virtual SpecialityFileSection SpecialityFileSection { get; set; }
    }
}
