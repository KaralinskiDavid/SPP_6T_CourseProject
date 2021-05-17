using Domain.Impl.Models.Request;
using Domain.Impl.Models.Response;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service
{
    public interface IUserService
    {
        public Task<GetUserResponseModel> GetUser(string userId);
        public Task<IEnumerable<GetUserResponseModel>> GetUsers();
        public Task<PostUserResponseModel> CreateUser(PostUserRequestModel request);
        public Task<PutUserResponseModel> UpdateUser(PutUserRequestModel request);
        public Task<bool?> DeleteUser(string userId);
    }
}
