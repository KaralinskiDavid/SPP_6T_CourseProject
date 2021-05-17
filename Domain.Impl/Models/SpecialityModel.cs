using Domain.Models;
using System.Collections.Generic;

namespace Domain.Impl.Models
{
    public class SpecialityModel : ISpecialityModel
    {
        public int Id { get; set; }
        public int FacultyId { get; set; }
        public string Name { get; set; }
        public string Abbreviature { get; set; }
        public string Code { get; set; }
        public int? HeadStudentId { get; set; }

        public FacultyModel Faculty { get; set; }
        public ICollection<GroupModel> Groups { get; set; }
        public StudentModel HeadStudent { get; set; }
    }
}
