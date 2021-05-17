
using Domain.Impl.Models.Response;
using System.Threading.Tasks;

namespace Service
{
    public interface IScheduleService
    {
        public Task<GetScheduleResponseModel> GetScheduleByGroupNumber(string groupNumber);
        public Task<bool> RefreshGroupSchedule(string groupNumber);
        public Task<int> GetCurrentWeek();
    }
}
