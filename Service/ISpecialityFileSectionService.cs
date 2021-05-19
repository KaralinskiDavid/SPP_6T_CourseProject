using Domain.Impl.Models;
using Domain.Impl.Models.Request;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public interface ISpecialityFileSectionService
    {
        public Task<bool> DeleteSpecialityFileSection(int id);
        public Task<List<SpecialityFileSectionModel>> GetSpecialityFileSections(int specialityId);
        public Task<bool> CreateSpecialityFileSection(PostSpecialityFileSectionRequestModel request);
    }
}
