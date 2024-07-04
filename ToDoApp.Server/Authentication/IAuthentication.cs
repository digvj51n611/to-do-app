using System.Drawing;
using ToDoApp.Server.Authentication.Models;
using ToDoApp.Service.Models;

namespace ToDoApp.Server.Authentication
{
    public interface IAuthentication
    {
        public Task<AuthResponse> LoginUserAsync(UserDto user);
    }
}
