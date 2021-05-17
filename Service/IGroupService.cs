using Domain.Impl.Models.Response;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public interface IGroupService
    {
        public Task<bool> RefreshGroups();
        public Task<List<GetGroupResponseModel>> GetGroups();
        public Task<bool> CheckGroupNumber(string groupNumber);
    }
}
