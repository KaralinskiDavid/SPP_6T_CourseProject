using System.Threading.Tasks;
using System;
using System.Linq;
using AutoMapper;
using System.Collections.Generic;
using Dao.Impl.DaoModels;
using Dao;
using Domain.Impl.Models.Response;
using Microsoft.EntityFrameworkCore;

namespace Service.Impl
{
    public class SpecialityService : ISpecialityService
    {
        private readonly IMapper _mapper;
        private readonly IBsuirIisApiService _bsuirIisApiService;
        private readonly ISpecialityDao<Speciality> _specialityDao;
        private readonly IFacultyDao<Faculty> _facultyDao;

        public SpecialityService(IMapper mapper, IBsuirIisApiService bsuirIisApiService,
            ISpecialityDao<Speciality> specialityDao, IFacultyDao<Faculty> facultyDao)
        {
            _mapper = mapper;
            _bsuirIisApiService = bsuirIisApiService;
            _specialityDao = specialityDao;
            _facultyDao = facultyDao;
        }

        public async Task<List<GetSpecialityResponseModel>> GetSpecialities() => _mapper.Map<List<GetSpecialityResponseModel>>(await (await _specialityDao.GetItemsAsync())
            .Include(s=>s.Faculty).Include(s=>s.HeadStudent).Include(s=>s.Groups).ToListAsync());

        public async Task<bool> RefreshSpecialities()
        {
            try
            {
                var returnedSpecialities = _mapper.Map<List<Speciality>>(await _bsuirIisApiService.GetSpecialities());
                var existingSpecialitiesIds = (await _specialityDao.GetItemsAsync()).Select(s=>s.Id);

                var newSpecialities = returnedSpecialities?.Where(s=>!existingSpecialitiesIds.Contains(s.Id));
                if (newSpecialities != null)
                {
                    var facultyIds = (await _facultyDao.GetItemsAsync()).Select(s => s.Id);
                    newSpecialities = newSpecialities.Where(s => facultyIds.Contains(s.FacultyId));
                    if (newSpecialities != null && newSpecialities.Any())
                        await _specialityDao.CreateRangeAsync(newSpecialities);
                }    
                

                var updatedSpecialities = returnedSpecialities?.Where(s=>existingSpecialitiesIds.Contains(s.Id));
                if (updatedSpecialities != null)
                {
                    var facultyIds = (await _facultyDao.GetItemsAsync()).Select(s => s.Id);
                    updatedSpecialities = updatedSpecialities.Where(s => facultyIds.Contains(s.FacultyId));
                    if (updatedSpecialities != null && updatedSpecialities.Any())
                        await _specialityDao.UpdateRangeAsync(updatedSpecialities);

                }
                    

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }
    }
}
