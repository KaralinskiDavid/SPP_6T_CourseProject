using Domain.Impl.Models.Request;
using Domain.Impl.Models.Response;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using Dto.Identity;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Dao;
using Dao.Impl.DaoModels;

namespace Service.Impl
{
    public class UserService : IUserService
    {
        private readonly UserManager<LearningAssistantUser> _userManager;
        private readonly IMapper _mapper;
        private readonly IStudentDao<Student> _studentDao;
        private readonly IGroupDao<Group> _groupDao;

        public UserService(UserManager<LearningAssistantUser> userManager, IMapper mapper, IStudentDao<Student> studentDao, IGroupDao<Group> groupDao)
        {
            _userManager = userManager;
            _mapper = mapper;
            _studentDao = studentDao;
            _groupDao = groupDao;
        }

        public Task<PostUserResponseModel> CreateUser(PostUserRequestModel request)
        {
            throw new NotImplementedException();
        }

        public async Task<bool?> DeleteUser(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if(user!=null)
                try
                {
                    if (!(await _userManager.IsInRoleAsync(user, "Admin")))
                        await _studentDao.RemoveAsync(await _studentDao.GetStudentByUserId(userId));
                    await _userManager.DeleteAsync(user);
                    return true;
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return false;
                }
            return null;
        }

        public async Task<GetUserResponseModel> GetUser(string userId) =>
            _mapper.Map<GetUserResponseModel>(await _userManager.FindByIdAsync(userId));

        public async Task<IEnumerable<GetUserResponseModel>> GetUsers()=>
            _mapper.Map<IList<GetUserResponseModel>>(await _userManager.Users.ToListAsync());

        public async  Task<bool?> Update(string userId, PutUserRequestModel request)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(userId);
                if (user == null)
                    return null;
                if (!(await _userManager.IsInRoleAsync(user, request.RoleName)))
                {
                    await _userManager.RemoveFromRoleAsync(user, (await _userManager.GetRolesAsync(user))[0]);
                    await _userManager.AddToRoleAsync(user, request.RoleName);
                }

                var student = await _studentDao.GetStudentByUserId(userId);
                if(student.Group.Number != request.GroupNumber || student.SubGroup!=request.SubGroup)
                {
                    student.SubGroup = request.SubGroup;
                    student.GroupId = (await _groupDao.GetGroupByNumber(request.GroupNumber)).Id;
                    await _studentDao.UpdateAsync(student);
                }
                return true;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public Task<PutUserResponseModel> UpdateUser(PutUserRequestModel request)
        {
            throw new NotImplementedException();
        }
    }
}
