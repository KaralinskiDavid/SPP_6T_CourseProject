namespace Domain.Models.IisApi
{
    public interface IGroupModel
    {
        int Id { get; set; }
        string Name { get; set; }
        int FacultyId { get; set; }
        int SpecialityDepartmentEducationFormId { get; set; }
        int Course { get; set; }
    }
}
