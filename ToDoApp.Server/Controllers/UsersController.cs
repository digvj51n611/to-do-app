using Microsoft.AspNetCore.Mvc;
using ToDoApp.data;
using ToDoApp.Service.Models;
using ToDoApp.Service.IServices;
using System.Configuration;

namespace ToDoApp.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly ToDoDbContext _context;
        private readonly IUserService _userService;
        public UsersController(ToDoDbContext context,IUserService service)
        {
            _context = context;
            _userService = service;
        }
        private ActionResult<T> ResultFromCode<T>(ErrorCode? code, Exception ex)
        {
            Console.WriteLine(code);
            if (code == ErrorCode.NotFoundError) return NotFound( new { error = ex.Message});
            if (code == ErrorCode.AuthenticationError) return Unauthorized("Unauthorized");
            if (code == ErrorCode.ValidationError) return BadRequest(new { validationErrors = ex.Message});
            return Problem(
                detail: ex.Message,
                statusCode: StatusCodes.Status500InternalServerError,
                title: "An unexpected error occurred.");
        }

        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetUsers()
        {
            var serviceResult = await _userService.GetUsersServiceAsync();
            if (serviceResult.IsSuccess)
            {
                return Ok(serviceResult.Result);
            }
            else
            {
                return ResultFromCode<IEnumerable<UserDto>>(serviceResult.ErrorCode, serviceResult.Exception!);
            }
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserDto>> GetUser(int id)
        {
            var serviceResult = await _userService.GetUserServiceAsync(id);
            if (serviceResult.IsSuccess)
            {
                return Ok(serviceResult.Result);
            }
            else
            {
                return ResultFromCode<UserDto>(serviceResult.ErrorCode, serviceResult.Exception!);
            }
        }

        // PUT: api/Users/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<ActionResult<UserDto>> PutUser(UserDto userDto)
        {
            var serviceResult = await _userService.UpdateUserServiceAsync(userDto);
            if (serviceResult.IsSuccess)
            {
                return Ok(serviceResult.Result);
            }
            else
            {
                return ResultFromCode<UserDto>(serviceResult.ErrorCode, serviceResult.Exception!);
            }
        }

        // POST: api/Users/register
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("/register")]
        public async Task<ActionResult<UserDto>> PostUser(UserDto userDto)
        {
            var serviceResult = await _userService.AddUserServiceAsync(userDto);
            if (serviceResult.IsSuccess)
            {
                return Ok(serviceResult.Result);
            }
            else
            {
                return ResultFromCode<UserDto>(serviceResult.ErrorCode, serviceResult.Exception!);
            }
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<UserDto>> DeleteUser(int id)
        {
            var serviceResult = await _userService.DeleteUserServiceAsync(id);
            if (serviceResult.IsSuccess)
            {
                return Ok(serviceResult.Result);
            }
            else
            {
                return ResultFromCode<UserDto>(serviceResult.ErrorCode, serviceResult.Exception!);
            }
        }

        //private bool UserExists(int id)
        //{
        //    return _context.Users.Any(e => e.Id == id);
        //}
    }
}
