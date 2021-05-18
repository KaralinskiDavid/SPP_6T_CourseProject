using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Impl.Models.Request
{
    public class PutUserRequestModel
    {
        public string RoleName { get; set; }
        public string GroupNumber { get; set; }
        public int? SubGroup { get; set; }
    }
}
