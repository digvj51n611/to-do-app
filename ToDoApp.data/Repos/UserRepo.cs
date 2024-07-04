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
        public async Task<User> GetUserAsync(string username)
        {
            using (var context = _context)
            {
                try
                {
                    var user = await context.Users.FirstOrDefaultAsync(user => user.Username == username);
                    if (user == null) throw new Exception("NotFoundError");
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
        public async Task<User> AddUserAsync(User user)
        {
            using (var context = _context)
            {
                try
                {
                    context.Users.Add(user);
                    try
                    {
                        if (await context.SaveChangesAsync() > 0)
                        {
                            return context.Users.FirstOrDefault(userData => userData.Username == user.Username)!;
                        }
                        throw new Exception("Save changes failed");
                    }
                    catch (Exception ex)
                    {
                        throw;
                    }

                }
                catch
                {
                    Console.WriteLine("Something wrong Babe");
                    throw;
                }
            }
        }
        public async Task<User> UpdateUserAsync(User user)
        {
            using (var context = _context)
            {
                try
                {
                    var oldUser = await GetUserAsync(user.Username);
                    oldUser.Username = user.Username;
                    oldUser.Password = user.Password;
                    if (await context.SaveChangesAsync() > 0)
                    {
                        return context.Users.FirstOrDefault(user => user.Username == oldUser.Username)!;
                    }
                    throw new Exception("User Update Failed");
                }
                catch (Exception ex)
                {
                    throw;
                }
            }
        }
        public async Task<User> DeleteUserAsync(string username)
        {
            using (var context = _context)
            {
                try
                {
                    var user = await GetUserAsync(username);
                    context.Remove(user);
                    if (await context.SaveChangesAsync() > 0)
                    {
                        return user;
                    }
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
