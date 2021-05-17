using System.Collections.Generic;

namespace Domain.Impl.Models.Response
{
    public class GetFacultyResponseModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Abbreviation { get; set; }

        public ICollection<SpecialityModel> Specialities { get; set; }
    }
}
