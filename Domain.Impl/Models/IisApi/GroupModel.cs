using Domain.Models.IisApi;

namespace Domain.Impl.Models.IisApi
{
    public class GroupModel : IGroupModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int FacultyId { get; set; }
        public int SpecialityDepartmentEducationFormId { get; set; }
        public int Course { get; set; }
    }
}
