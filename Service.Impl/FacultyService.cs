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
    public class FacultyService : IFacultyService
    {
        private readonly IMapper _mapper;
        private readonly IBsuirIisApiService _bsuirIisApiService;
        private readonly IFacultyDao<Faculty> _facultyDao;

        public FacultyService(IMapper mapper, IBsuirIisApiService bsuirIisApiService, IFacultyDao<Faculty> facultyDao)
        {
            _mapper = mapper;
            _bsuirIisApiService = bsuirIisApiService;
            _facultyDao = facultyDao;
        }

        public async Task<IList<GetFacultyResponseModel>> GetFaculties() =>
            _mapper.Map<IList<GetFacultyResponseModel>>((await _facultyDao.GetItemsAsync()).Include(f => f.Specialities).ToList());

        public async Task<bool> RefreshFaculties()
        {
            try
            {
                var returnedFaculties = _mapper.Map<List<Faculty>>(await _bsuirIisApiService.GetFaculties());
                var existingFacultiesIds = (await _facultyDao.GetItemsAsync()).Select(f=>f.Id);

                var newFaculties = returnedFaculties?.Where(f=>!existingFacultiesIds.Contains(f.Id));
                if(newFaculties!=null)
                    await _facultyDao.CreateRangeAsync(newFaculties);

                var updatedFaculties = returnedFaculties?.Where(f=>existingFacultiesIds.Contains(f.Id));
                if(updatedFaculties!=null)
                    await _facultyDao.UpdateRangeAsync(updatedFaculties);

                return true;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }
    }
}
