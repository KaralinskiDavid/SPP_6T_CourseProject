using System.Collections.Generic;

namespace Domain.Impl.Models.Response
{
    public class GetGroupResponseModel
    {
        public int Id { get; set; }
        public int Course { get; set; }
        public int SpecialityId { get; set; }
        public string Number { get; set; }
        public int HeadStudentId { get; set; }

        public SpecialityModel Speciality { get; set; }
        public StudentModel Student { get; set; }
        public ICollection<StudentModel> Students { get; set; }
    }
}
