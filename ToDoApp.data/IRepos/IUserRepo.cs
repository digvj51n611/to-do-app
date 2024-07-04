using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoApp.Data.Entities;

namespace ToDoApp.Data.IRepos
{
    public interface IUserRepo
    {
        public Task<User> GetUserAsync(string username);
        public Task<List<User>> GetUsersAsync();
        public Task<User> AddUserAsync(User user);
        public Task<User> UpdateUserAsync(User user);
        public Task<User> DeleteUserAsync(string username);
    }
}
