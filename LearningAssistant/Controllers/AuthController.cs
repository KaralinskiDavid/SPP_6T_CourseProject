using Domain.Impl.Models.Request;
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
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost]
        [Route("Login")]
        public async Task<ActionResult<PostLoginResponseModel>> Login([FromBody] PostLoginRequestModel request)
        {
            var result = await _authService.LoginAsync(request, HttpContext.Connection?.RemoteIpAddress.ToString());
            if (result != null)
                return Ok(result);
            return BadRequest();
        }

        [HttpPost]
        [Route("Register")]
        public async Task<ActionResult<bool>> Register([FromBody] PostRegisterRequestModel request)
        {
            var result = await _authService.RegisterAsync(request);
            return result;
        }

        [HttpGet]
        [Route("Check/{userEmail}")]
        public async Task<ActionResult<bool>> CheckUserEmail([FromRoute] string userEmail)
        {
            var result = await _authService.CheckUserEmail(userEmail);
            return Ok(result);
        }

        [HttpPost]
        [Route("refresh/{refreshToken}")]
        public async Task<ActionResult> RefreshToken([FromRoute] string refreshToken)
        {
            var response = await _authService.RefreshToken(refreshToken, HttpContext.Connection?.RemoteIpAddress.ToString());
            if (response == null)
                return Unauthorized();
            else
                return Ok(response);
        }

    }
}
