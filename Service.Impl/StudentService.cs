using AutoMapper;
using Dao;
using Dao.Impl.DaoModels;
using Domain.Impl.Models;
using Domain.Impl.Models.Response;
using Dto.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace Service.Impl
{
    public class StudentService : IStudentService
    {
        private readonly UserManager<LearningAssistantUser> _userManager;
        private readonly IStudentDao<Student> _studentDao;
        private readonly IMapper _mapper;

        public StudentService(UserManager<LearningAssistantUser> userManager, IStudentDao<Student> studentDao, IMapper mapper)
        {
            _userManager = userManager;
            _studentDao = studentDao;
            _mapper = mapper;
        }


        public Task<bool> CreateStudent(StudentModel studentModel)
        {
            throw new System.NotImplementedException();
        }

        public async Task<bool> DeleteStudent(int studentId)
        {
            try
            {
                var student = await _studentDao.GetItemByIdAsync(studentId);
                if (student == null)
                    return false;
                var identityResult = await _userManager.DeleteAsync(await _userManager.FindByIdAsync(student.UserId));
                if (!identityResult.Succeeded)
                    return false;
                var removeResult = await _studentDao.RemoveAsync(student);
                return true;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public Task<GetStudentResponseModel> GetStudent(int id)
        {
            throw new System.NotImplementedException();
        }

        public async Task<IEnumerable<GetStudentResponseModel>> GetStudents()
        {
            List<GetStudentResponseModel> result = new List<GetStudentResponseModel>();
            try
            {
                var students = (await _studentDao.GetItemsAsync()).Include(s=>s.Group).ThenInclude(g=>g.Speciality);
                foreach(var student in students)
                {
                    var user = await _userManager.FindByIdAsync(student.UserId);
                    var responseModel = _mapper.Map<GetStudentResponseModel>(student);
                    responseModel.UserEmail = user.Email;
                    responseModel.UserFirstName = user.FirstName;
                    responseModel.UserLastName = user.LastName;
                    responseModel.UserMiddleName = user.MiddleName;
                    responseModel.Group = _mapper.Map<GroupModel>(student.Group);
                    responseModel.Speciality = _mapper.Map<SpecialityModel>(student.Group.Speciality);
                    responseModel.SubGroup = student.SubGroup;
                    responseModel.RoleName = (await _userManager.GetRolesAsync(user)).FirstOrDefault();
                    result.Add(responseModel);
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
            return result;
        }

        public async Task<IEnumerable<GetStudentResponseModel>> GetStudentsByGroupNumber(string groupNumber)
        {
            List<GetStudentResponseModel> result = new List<GetStudentResponseModel>();
            try
            {
                var students = (await _studentDao.GetItemsAsync()).Include(s => s.Group).ThenInclude(g => g.Speciality).Where(s=>s.Group.Number==groupNumber);
                foreach (var student in students)
                {
                    var user = await _userManager.FindByIdAsync(student.UserId);
                    var responseModel = _mapper.Map<GetStudentResponseModel>(student);
                    responseModel.UserEmail = user.Email;
                    responseModel.UserFirstName = user.FirstName;
                    responseModel.UserLastName = user.LastName;
                    responseModel.UserMiddleName = user.MiddleName;
                    responseModel.Group = _mapper.Map<GroupModel>(student.Group);
                    responseModel.Speciality = _mapper.Map<SpecialityModel>(student.Group.Speciality);
                    responseModel.SubGroup = student.SubGroup;
                    result.Add(responseModel);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
            return result;
        }



        public Task<PutStudentResponseModel> UpdateStudent()
        {
            throw new System.NotImplementedException();
        }
    }
}
