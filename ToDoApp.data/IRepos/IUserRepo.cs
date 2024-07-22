using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoApp.Data.Entities;
using ToDoApp.Data.Models;

namespace ToDoApp.Data.IRepos
{
    public interface IUserRepo : IGenericRepo<User>
    {
        public Task<DataResponse<User>> GetUserAsync(int id);
        public Task<DataResponse<List<User>>> GetUsersAsync();
        public Task<DataResponse<User>> AddUserAsync(User user);
        public Task<DataResponse<User>> UpdateUserAsync(User user);
        public Task<DataResponse<User>> DeleteUserAsync(int id);
    }
}
