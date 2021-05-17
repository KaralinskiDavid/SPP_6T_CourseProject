using System.Threading.Tasks;
using System.Collections.Generic;
using System.Text;
using Domain.Impl.Models.Response;

namespace Service
{
    public interface IFacultyService
    {
        public Task<bool> RefreshFaculties();
        public Task<IList<GetFacultyResponseModel>> GetFaculties();
    }
}
