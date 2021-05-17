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
    public class GroupService : IGroupService
    {
        private readonly IMapper _mapper;
        private readonly IBsuirIisApiService _bsuirIisApiService;
        private readonly IGroupDao<Group> _groupDao;
        private readonly ISpecialityDao<Speciality> _specialityDao;
        
        public GroupService(IMapper mapper, IBsuirIisApiService bsuirIisApiService, IGroupDao<Group> groupDao,
            ISpecialityDao<Speciality> specialityDao)
        {
            _mapper = mapper;
            _bsuirIisApiService = bsuirIisApiService;
            _groupDao = groupDao;
            _specialityDao = specialityDao;
        }

        public async Task<bool> CheckGroupNumber(string groupNumber) => (await _groupDao.GetGroupByNumber(groupNumber)) != null;

        public async Task<List<GetGroupResponseModel>> GetGroups()
        {
            var groups = await (await _groupDao.GetItemsAsync()).Include(g => g.Student).Include(g => g.Students).Include(g => g.Speciality).Include(s => s.Speciality.Faculty)
                .ToListAsync();
            return _mapper.Map<List<GetGroupResponseModel>>(groups);
        }

        public async Task<bool> RefreshGroups()
        {
            try
            {
                var returnedGroups = _mapper.Map<List<Group>>(await _bsuirIisApiService.GetGroups());
                var existingGroups = (await _groupDao.GetItemsAsync()).AsNoTracking();
                var existingGroupsIds = existingGroups.Select(g=>g.Id);

                var newGroups = returnedGroups?.Where(g=>!existingGroupsIds.Contains(g.Id));
                if (newGroups != null) 
                {
                    var specialityIds = (await _specialityDao.GetItemsAsync()).Select(s => s.Id);
                    newGroups = newGroups.Where(g => specialityIds.Contains(g.SpecialityId));
                    if(newGroups!=null && newGroups.Any())
                        await _groupDao.CreateRangeAsync(newGroups);
                }
                    

                var updatedGroups = returnedGroups?.Where(x=> 
                {
                    var toUpdate = existingGroups.FirstOrDefault(g => g.Id == x.Id);
                    if (toUpdate == null || !toUpdate.Equals(x))
                        return true;
                    return false;
                });
              
                if (updatedGroups != null)
                    await _groupDao.UpdateRangeAsync(updatedGroups);

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

