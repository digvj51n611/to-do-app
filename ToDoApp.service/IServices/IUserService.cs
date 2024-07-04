using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoApp.Data.Entities;
using ToDoApp.Service.Models;

namespace ToDoApp.Service.IServices
{
    public interface IUserService
    {
        public Task<ServiceResult<UserDto>> GetUserServiceAsync(string username);
        public Task<ServiceResult<List<UserDto>>> GetUsersServiceAsync();
        public Task<ServiceResult<UserDto>> AddUserServiceAsync(UserDto user);
        public Task<ServiceResult<UserDto>> UpdateUserServiceAsync(UserDto user);
        public Task<ServiceResult<UserDto>> DeleteUserServiceAsync(string username);
    }
}
