namespace Domain.Models
{
    public interface IGroupModel
    {
        int Id { get; set; }
        int Course { get; set; }
        int SpecialityId { get; set; }
        string Number { get; set; }
        int HeadStudentId { get; set; }
    }
}
