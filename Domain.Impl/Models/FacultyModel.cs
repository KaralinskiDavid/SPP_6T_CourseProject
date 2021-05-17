using Domain.Models;

namespace Domain.Impl.Models
{
    public class FacultyModel : IFacultyModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Abbreviation { get; set; }
    }
}
