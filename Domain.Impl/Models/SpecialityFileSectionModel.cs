using Domain.Impl.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Impl.Models
{
    public class SpecialityFileSectionModel
    {
        public int Id { get; set; }
        public int SpecialityId { get; set; }
        public string Name { get; set; }

        public SpecialityModel Speciality { get; set; }
        public List<FileModel> SpecialityFiles { get; set; }
    }
}
