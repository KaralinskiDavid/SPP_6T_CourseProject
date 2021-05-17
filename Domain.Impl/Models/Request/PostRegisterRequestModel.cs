namespace Domain.Impl.Models.Request
{
    public class PostRegisterRequestModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Middlename { get; set; }
        public string GroupNumber { get; set; }
        public int SubGroup { get; set; }
        public string RoleName { get; set; }
    }
}
