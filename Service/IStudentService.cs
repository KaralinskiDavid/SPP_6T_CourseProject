using Domain.Impl.Models;
using Domain.Impl.Models.Response;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service
{
    public interface IStudentService
    {
        public Task<GetStudentResponseModel> GetStudent(int id);
        public Task<IEnumerable<GetStudentResponseModel>> GetStudents();
        public Task<IEnumerable<GetStudentResponseModel>> GetStudentsByGroupNumber(string groupNumber);
        public Task<PutStudentResponseModel> UpdateStudent();
        public Task<bool> CreateStudent(StudentModel studentModel);
        public Task<bool> DeleteStudent(int studentId);
    }
}
