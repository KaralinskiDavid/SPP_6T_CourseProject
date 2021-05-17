namespace Domain.Impl.Models.Response
{
    public class PostLoginResponseModel
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public string Token { get; set; }
        public string RefreshToken { get; set; }
    }
}
