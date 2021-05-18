using Domain.Impl.Models.Request;
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
    public class QueueController : ControllerBase
    {
        private readonly IQueueService _queueService;

        public QueueController(IQueueService queueService)
        {
            _queueService = queueService;
        }

        [HttpPost]
        [Authorize(Roles ="GroupHeadman")]
        public async Task<ActionResult<bool>> CreateQueue([FromBody] PostQueueRequestModel request)
        {
            var result = await _queueService.CreateQueue(request);
            return Ok(result);
        }

        [HttpDelete]
        [Route("{queueId}")]
        [Authorize(Roles = "GroupHeadman")]
        public async Task<ActionResult<bool>> DeleteQueue([FromRoute] int queueId)
        {
            var result = await _queueService.DeleteQueue(queueId);
            return Ok(result);
        }

    }
}
