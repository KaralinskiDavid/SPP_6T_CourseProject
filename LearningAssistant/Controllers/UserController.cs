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
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }


        [HttpDelete]
        [Route("{userId}")]
        public async Task<ActionResult<bool>> DeleteUser([FromRoute] string userId)
        {
            var result = await _userService.DeleteUser(userId);
            if (result != null)
                return Ok(result);
            return BadRequest();
        }

        [HttpPut]
        [Route("{userId}")]
        public async Task<ActionResult<bool>> UpdateUser([FromRoute] string userId, [FromBody] PutUserRequestModel request)
        {
            var result = await _userService.Update(userId, request);
            if (result == null)
                return BadRequest();
            return Ok(result);
        }

    }
}
