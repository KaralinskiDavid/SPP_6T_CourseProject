using Domain.Impl.Models.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LearningAssistant.Controllers
{
    [Route("api/[controller]s")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IStudentService _studentService;

        public StudentController(IStudentService studentService)
        {
            _studentService = studentService;
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<IEnumerable<GetStudentResponseModel>>> GetStudents()
        {
            var result = await _studentService.GetStudents();
            return Ok(result);
        }

        [HttpGet]
        [Route("{groupNumber}")]
        public async Task<ActionResult<IEnumerable<GetStudentResponseModel>>> GetStudentsByGroupNumber([FromRoute] string groupNumber)
        {
            var result = await _studentService.GetStudentsByGroupNumber(groupNumber);
            return Ok(result);
        }

        [HttpDelete]
        [Route("{studentId}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<bool>> DeleteStudent(int studentId)
        {
            var result = await _studentService.DeleteStudent(studentId);
            return Ok(result);
        }

    }
}
