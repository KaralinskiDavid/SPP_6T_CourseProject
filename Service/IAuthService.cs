using Domain.Impl.Models.Request;
using Domain.Impl.Models.Response;
using Dto.Identity;
using System.Threading.Tasks;

namespace Service
{
    public interface IAuthService
    {
        public Task<PostLoginResponseModel> LoginAsync(PostLoginRequestModel request, string ipAddress);
        public Task<bool> RegisterAsync(PostRegisterRequestModel request);
        public Task<bool> CheckUserEmail(string userEmail);
        string GenerateJwtToken(LearningAssistantUser user);
        public Task<PostLoginResponseModel> RefreshToken(string refreshToken, string ip);
    }
}
