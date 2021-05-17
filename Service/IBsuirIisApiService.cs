using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Impl.Models.IisApi;

namespace Service
{
    public interface IBsuirIisApiService
    {
        public Task<int> GetCurrentWeek();
        public Task<IList<FacultyModel>> GetFaculties();
        public Task<IList<SpecialityModel>> GetSpecialities();
        public Task<IList<GroupModel>> GetGroups();
        public Task<ScheduleModel> GetScheduleByGroupNumber(string groupNumber);
    }
}
