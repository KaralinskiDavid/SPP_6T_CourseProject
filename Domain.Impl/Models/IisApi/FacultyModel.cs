using Domain.Models.IisApi;

namespace Domain.Impl.Models.IisApi
{
    public class FacultyModel : IFacultyModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Abbrev { get; set; }
    }
}
