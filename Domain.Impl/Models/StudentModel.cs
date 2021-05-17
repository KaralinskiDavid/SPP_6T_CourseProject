using Domain.Models;

namespace Domain.Impl.Models
{
    public class StudentModel : IStudentModel
    {
        public int Id { get; set ; }
        public int GroupId { get; set; }
        public string UserId { get; set; }

        //public GroupModel Group { get; set; }
    }
}
