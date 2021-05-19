using System.Threading.Tasks;
using System;
using System.Linq;
using AutoMapper;
using System.Collections.Generic;
using Dao.Impl.DaoModels;
using Dao;
using Domain.Impl.Models.Response;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Dto.Identity;

namespace Service.Impl
{
    public class SpecialityService : ISpecialityService
    {
        private readonly IMapper _mapper;
        private readonly IBsuirIisApiService _bsuirIisApiService;
        private readonly ISpecialityDao<Speciality> _specialityDao;
        private readonly IFacultyDao<Faculty> _facultyDao;
        private readonly UserManager<LearningAssistantUser> _userManager;

        public SpecialityService(IMapper mapper, IBsuirIisApiService bsuirIisApiService,
            ISpecialityDao<Speciality> specialityDao, IFacultyDao<Faculty> facultyDao, UserManager<LearningAssistantUser> userManager)
        {
            _mapper = mapper;
            _bsuirIisApiService = bsuirIisApiService;
            _specialityDao = specialityDao;
            _facultyDao = facultyDao;
            _userManager = userManager;
        }

        public async Task<List<GetSpecialityResponseModel>> GetSpecialities()
        {
            var specialities = _mapper.Map<List<GetSpecialityResponseModel>>(await (await _specialityDao.GetItemsAsync())
            .Include(s => s.Faculty).Include(s => s.HeadStudent).Include(s => s.Groups).ToListAsync());
            foreach(var speciality in specialities.Where(s=>s.HeadStudentId!=null))
            {
                var user = await _userManager.FindByIdAsync(speciality.HeadStudent.UserId);
                speciality.HeadStudentName = user.LastName + " " + user.FirstName;
            }
            return specialities;
        }

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
