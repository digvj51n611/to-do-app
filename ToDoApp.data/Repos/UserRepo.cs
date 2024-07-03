using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoApp.data;
using ToDoApp.Data.Entities;
using ToDoApp.Data.IRepos;

namespace ToDoApp.Data.Repos
{
    public class UserRepo : IUserRepo
    {
        private ToDoDbContext _context;
        public UserRepo(ToDoDbContext context)
        {
            _context = context;
        }
        public async Task<User> GetUserAsync(int id)
        {
            using (var context = _context)
            {
                try
                {
                    var user = await context.Users.FindAsync(id);
                    if (user == null) throw new Exception("Not Found");
                    return user;
                }
                catch (Exception ex)
                {
                    throw;
                }
            }
        }
        public async Task<List<User>> GetUsersAsync()
        {
            using (var context = _context)
            {
                try
                {
                    return await context.Users.ToListAsync<User>();
                }
                catch (Exception ex)
                {
                    throw;
                }
            }
        }
        public async Task<bool> AddUserAsync(User user)
        {
            using (var context = _context)
            {
                try
                {
                    context.Users.Add(user);
                    try
                    {
                        if (await context.SaveChangesAsync() > 0)
                            return true;
                        throw new Exception("Save changes failed");
                    }
                    catch (Exception ex)
                    {
                        throw;
                    }

                }
                catch
                {
                    throw;
                }
            }
        }
        public async Task<bool> UpdateUserAsync(User user)
        {
            using (var context = _context)
            {
                try
                {
                    var oldUser = await GetUserAsync(user.Id);
                    oldUser.Username = user.Username;
                    oldUser.Password = user.Password;
                    if (await context.SaveChangesAsync() > 0) return true;
                    throw new Exception("User Update Failed");
                }
                catch (Exception ex)
                {
                    throw;
                }
            }
        }
        public async Task<bool> DeleteUserAsync(int id)
        {
            using (var context = _context)
            {
                try
                {
                    var user = await GetUserAsync(id);
                    context.Remove(user);
                    if (await context.SaveChangesAsync() > 0) return true;
                    throw new Exception("User Deletion Failed!");
                }
                catch
                {
                    throw;
                }
            }
        }
    }
}
