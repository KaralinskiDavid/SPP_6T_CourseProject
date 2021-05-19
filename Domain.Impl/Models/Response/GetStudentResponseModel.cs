using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Impl.Models.Response
{
    public class GetStudentResponseModel
    {
        public int Id { get; set; }
   
        public int GroupId { get; set; }
        public int? SubGroup { get; set; }
        public string UserId { get; set; }
        public int? SpecialityId { get; set; }
        public string UserEmail { get; set; }
        public string UserFirstName { get; set; }
        public string UserLastName { get; set; }
        public string UserMiddleName { get; set; }
        public string RoleName { get; set; }
        public virtual GroupModel Group { get; set; }
        public virtual SpecialityModel Speciality { get; set; }
    }
}
