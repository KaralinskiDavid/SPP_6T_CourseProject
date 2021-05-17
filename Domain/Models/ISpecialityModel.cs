namespace Domain.Models
{
    public interface ISpecialityModel
    {
        int Id { get; set; }
        int FacultyId { get; set; }
        string Name { get; set; }
        string Abbreviature { get; set; }
        string Code { get; set; }
        int? HeadStudentId { get; set; }
    }
}
