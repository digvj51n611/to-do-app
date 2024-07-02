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
        public Task<User> GetUserAsync(int id);
        public Task<List<User>> GetUsersAsync();
        public Task<bool> AddUserAsync(User user);
        public Task<bool> UpdateUserAsync(User user);
        public Task<bool> DeleteUserAsync(int id);
    }
}
