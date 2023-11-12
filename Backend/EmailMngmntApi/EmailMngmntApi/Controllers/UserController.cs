using EmailMngmntApi.DTOs;
using EmailMngmntApi.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EmailMngmntApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("Create")]
        public async Task<bool> CreateAsync([FromBody] UserDTO userDTO)
        {
            return await _userService.CreateAsync(userDTO);
        }

        [HttpPost("Login")]
        public async Task<IActionResult> LoginAsync([FromBody] Login loginCredentials)
        {
            var token = await _userService.LoginAsync(loginCredentials);
            if (String.IsNullOrEmpty(token)==false)
            {
                return Ok(token);
            }
            else
            {
                return BadRequest(token);
            }
        }
    }
}
