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
    public class ScheduleController : ControllerBase
    {
        private readonly IScheduleService _scheduleService;

        public ScheduleController(IScheduleService scheduleService)
        {
            _scheduleService = scheduleService;
        }

        [HttpGet]
        [Route("currentWeek")]
        public async Task<ActionResult<int>> GetCurrentWeek()
        {
            var result = await _scheduleService.GetCurrentWeek();
            return Ok(result);
        }

        [HttpPost]
        [Route("{groupNumber}")]
        public async Task<ActionResult<GetScheduleResponseModel>> GetSchedulesForGroup([FromRoute] string groupNumber)
        {
            var result = await _scheduleService.GetScheduleByGroupNumber(groupNumber);
            return result == null ? BadRequest() : Ok(result);
        }

        [HttpPost]
        [Route("refresh/{groupNumber}")]
        public async Task<ActionResult> RefreshGroupSchedule([FromRoute] string groupNumber)
        {
            var result = await _scheduleService.RefreshGroupSchedule(groupNumber);
            if (result)
                return Ok();
            return BadRequest();
        }

    }
}
