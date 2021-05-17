namespace Domain.Models
{
    public interface IStudentModel
    {
        int Id { get; set; }
        int GroupId { get; set; }
        string UserId { get; set; }
    }
}
