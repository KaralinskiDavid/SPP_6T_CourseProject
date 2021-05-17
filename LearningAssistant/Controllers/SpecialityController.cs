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
    [Route("api/[controller]")]
    [ApiController]
    public class SpecialityController : ControllerBase
    {
        private readonly ISpecialityService _specialityService;

        public SpecialityController(ISpecialityService specialityService)
        {
            _specialityService = specialityService;
        }


        [HttpGet]
        public async Task<ActionResult<List<GetSpecialityResponseModel>>> GetSpecialities()
        {
            var result = await _specialityService.GetSpecialities();
            return Ok(result);
        }

        [HttpPost]
        [Route("refresh")]
        public async Task<IActionResult> Refresh()
        {
            var result = await _specialityService.RefreshSpecialities();
            if (result)
                return NoContent();
            return BadRequest();
        }
    }
}
