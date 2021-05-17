using Domain.Impl.Models.Response;
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
    public class FacultyController : ControllerBase
    {
        private readonly IFacultyService _facultyService;

        public FacultyController(IFacultyService facultyService)
        {
            _facultyService = facultyService;
        }


        [HttpGet]
        public async Task<ActionResult<List<GetFacultyResponseModel>>> GetFaculties()
        {
            var result = await _facultyService.GetFaculties();
            return Ok(result);
        }

        [HttpPost]
        [Route("refresh")]
        public async Task<IActionResult> Refresh ()
        {
            var result = await _facultyService.RefreshFaculties();
            if (result)
                return NoContent();
            return BadRequest();
        }

    }
}
