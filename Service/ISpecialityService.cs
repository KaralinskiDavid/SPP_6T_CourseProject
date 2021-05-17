using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Impl.Models.Response;

namespace Service
{
    public interface ISpecialityService
    {
        public Task<bool> RefreshSpecialities();
        public Task<List<GetSpecialityResponseModel>> GetSpecialities();
    }
}
