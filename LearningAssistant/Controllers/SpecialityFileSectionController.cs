using Domain.Impl.Models;
using Domain.Impl.Models.Request;
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
    public class SpecialityFileSectionController : ControllerBase
    {
        private readonly ISpecialityFileSectionService _specialityFileSectionService;

        public SpecialityFileSectionController(ISpecialityFileSectionService specialityFileSectionService)
        {
            _specialityFileSectionService = specialityFileSectionService;
        }

        [HttpGet]
        [Route("{specialityId}")]
        public async Task<ActionResult<List<SpecialityFileSectionModel>>> GetSpecialityFileSections([FromRoute] int specialityId)
        {
            var result = await _specialityFileSectionService.GetSpecialityFileSections(specialityId);
            return Ok(result);
        }

        [HttpDelete]
        [Route("{specialityId}")]
        public async Task<ActionResult<bool>> DeleteSpecialityFileSection([FromRoute] int specialityId)
        {
            var result = await _specialityFileSectionService.DeleteSpecialityFileSection(specialityId);
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<bool>> CreateSpecialityFileSection([FromBody] PostSpecialityFileSectionRequestModel request)
        {
            var result = await _specialityFileSectionService.CreateSpecialityFileSection(request);
            return Ok(result);
        }

    }
}
