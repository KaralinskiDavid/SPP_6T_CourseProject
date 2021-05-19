using AutoMapper;
using Dao;
using Dao.Impl.DaoModels;
using Domain.Impl.Models;
using Domain.Impl.Models.Request;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Service.Impl
{
    public class SpecialityFileSectionService : ISpecialityFileSectionService
    {
        private readonly IMapper _mapper;
        private readonly ISpecialityFileSectionDao<SpecialityFileSection> _specialityFileSectionDao;
        private readonly ISpecialityDao<Speciality> _specialityDao;

        public SpecialityFileSectionService(IMapper mapper, ISpecialityFileSectionDao<SpecialityFileSection> specialityFileSectionDao, ISpecialityDao<Speciality> specialityDao)
        {
            _mapper = mapper;
            _specialityDao = specialityDao;
            _specialityFileSectionDao = specialityFileSectionDao;
        }

        public async Task<bool> CreateSpecialityFileSection(PostSpecialityFileSectionRequestModel request)
        {
            try
            {
                await _specialityFileSectionDao.CreateAsync(_mapper.Map<SpecialityFileSection>(request));
                return true;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public async Task<bool> DeleteSpecialityFileSection(int id)
        {
            try
            {
                var section = await _specialityFileSectionDao.GetItemByIdAsync(id);
                if (section == null)
                    return false;
                await _specialityFileSectionDao.RemoveAsync(section);
                return true; 
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public async Task<List<SpecialityFileSectionModel>> GetSpecialityFileSections(int specialityId)
        {
            try
            {
                var speciality = (await _specialityDao.GetItemByIdAsync(specialityId));
                if (speciality == null)
                    return null;
                return _mapper.Map<List<SpecialityFileSectionModel>>(speciality.SpecialityFileSections);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }
    }
}
