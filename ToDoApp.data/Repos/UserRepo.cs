using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoApp.data;
using ToDoApp.Data.Entities;
using ToDoApp.Data.IRepos;
using ToDoApp.Data.Models;

namespace ToDoApp.Data.Repos
{
    public class UserRepo : IUserRepo
    {
        private ToDoDbContext _context;
        public UserRepo(ToDoDbContext context)
        {
            _context = context;
        }
        public async Task<DataResponse<User>> GetUserAsync(int id)
        {
            using (var context = _context)
            {
                try
                {
                    var user = await context.Users.FindAsync(id);
                    if (user == null)
                    {
                        string message = $"User with id : {id} Not found";
                        return DataResponse<User>.FailureResult(ErrorType.NotFoundError, message);
                    }
                    return DataResponse<User>.SuccessResult(user);
                }
                catch (Exception ex)
                {
                    return DataResponse<User>.FailureResult(ex);
                }
            }
        }
        public async Task<DataResponse<List<User>>> GetUsersAsync()
        {
            using (var context = _context)
            {
                try
                {
                    var list = await context.Users.ToListAsync<User>();
                    if (list.Any())
                    {

                        return DataResponse<List<User>>.SuccessResult(list);
                    }
                    string message = "Users not found";
                    return DataResponse<List<User>>.FailureResult(ErrorType.NotFoundError, message);
                }
                catch (Exception ex)
                {
                    return DataResponse<List<User>>.FailureResult(ex);
                }
            }
        }
        public async Task<DataResponse<User>> AddUserAsync(User user)
        {
            using (var context = _context)
            {
                try
                {
                    context.Users.Add(user);
                    try
                    {
                        if (await context.SaveChangesAsync() > 0)
                            return DataResponse<User>.SuccessResult(user);
                        string message = "Save Changes failed";
                        return DataResponse<User>.FailureResult(ErrorType.SaveChangesError, message);
                    }
                    catch (DbUpdateException ex)
                    {
                        return DataResponse<User>.FailureResult(ex);
                    }
                }
                catch (Exception ex)
                {
                    return DataResponse<User>.FailureResult(ex);
                }
            }
        }
        public async Task<DataResponse<User>> UpdateUserAsync(User user)
        {
            // We will nly try to access the username and get the ID from the username by writing a private method to it.
            // Then we will use the method for getting the id to get the user from the DB
            using (var context = _context)
            {
                try
                {
                    var userEntry = await context.Users.FindAsync(user.Id);
                    if (userEntry == null)
                    {
                        string message = $"User Not Found with id:{user.Id}";
                        return DataResponse<User>.FailureResult(ErrorType.NotFoundError, message);
                    }
                    userEntry.Username = user.Username;
                    userEntry.Password = user.Password;
                    
                    if (await context.SaveChangesAsync() > 0)
                    {
                        return DataResponse<User>.SuccessResult(userEntry);
                    }
                    return DataResponse<User>.FailureResult(ErrorType.SaveChangesError, "Save Changes Failed");

                }
                catch (Exception ex)
                {
                    return DataResponse<User>.FailureResult(ex);
                }
            }
        }
        public async Task<DataResponse<User>> DeleteUserAsync(int id)
        {
            using (var context = _context)
            {
                try
                {
                    var user = await _context.Users.FindAsync(id);
                    if (user == null)
                    {
                        string message = $"User with id:{id} Not Found";
                        return DataResponse<User>.FailureResult(ErrorType.NotFoundError, message);
                    }
                    context.Remove(user);
                    if (await context.SaveChangesAsync() > 0)
                    {
                        return DataResponse<User>.SuccessResult(user);
                    }
                    return DataResponse<User>.FailureResult(ErrorType.SaveChangesError, "Save Changes Failed");
                }
                catch (Exception ex)
                {
                    return DataResponse<User>.FailureResult(ex);
                }
            }
        }
    }
}
