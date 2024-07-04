using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NuGet.Configuration;
using ToDoApp.Server.Authentication;
using ToDoApp.Server.Authentication.Models;
using ToDoApp.Service.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ToDoApp.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        IAuthentication _authentication;
        public AuthController(IAuthentication authentication)
        {
            _authentication = authentication;
        }
        // POST api/<AuthController>
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> LoginPost(UserDto userDto)
        {
            var response = await _authentication.LoginUserAsync(userDto);
            if(response.IsSuccess)
            {
                return Ok(new { token = response.TokenString});
            }
            if( response.Message == "PasswordMismatch")
            {
                return BadRequest(response.Message);
            }
            return Problem(detail : response.Message, statusCode: 500);
        }
    }
}
