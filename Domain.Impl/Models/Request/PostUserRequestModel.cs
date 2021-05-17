using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Impl.Models.Request
{
    public class PostUserRequestModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
    }
}
