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
    public class GroupController : ControllerBase
    {
        private readonly IGroupService _groupService;

        public GroupController(IGroupService groupService)
        {
            _groupService = groupService;
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<List<GetGroupResponseModel>>> GetGroups()
        {
            var result = await _groupService.GetGroups();
            return Ok(result);
        }

        [HttpGet]
        [Authorize(Roles="Admin, GroupHeadman, SpecialityHeadman, Student")]
        [Route("{groupNumber}")]
        public async Task<ActionResult<GetGroupResponseModel>> GetGroupByNumber([FromRoute] string groupNumber)
        {
            var result = await _groupService.GetGroupByNumber(groupNumber);
            return Ok(result);
        }

        [HttpGet]
        [Route("check/{groupNumber}")]
        public async Task<ActionResult<bool>> CheckGroupNumber([FromRoute] string groupNumber)
        {
            var result = await _groupService.CheckGroupNumber(groupNumber);
            return Ok(result);
        }

        [HttpPost]
        [Route("refresh")]
        public async Task<IActionResult> Refresh()
        {
            var result = await _groupService.RefreshGroups();
            if (result)
                return NoContent();
            return BadRequest();
        }
    }
}
